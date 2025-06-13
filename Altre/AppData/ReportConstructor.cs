using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Altre.AppData
{
    internal class ReportConstructor
    {
        public static String[] Config; //TO DO
        public static bool isTimeInterval;
        public static bool isPayments;
        public static bool isGroupBy;

        public static void getReport(DateTime startDate, DateTime endDate)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application()
            {
                Visible = true,
                SheetsInNewWorkbook = 1
            };
            Microsoft.Office.Interop.Excel.Workbook work = app.Workbooks.Add(Type.Missing);
            app.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)app.Worksheets.get_Item(1);
            sheet.Name = "Отчёт";

            // Заголовок отчета
            sheet.Cells[1, 1] = $"Отчет по выплатам за период с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}";

            // Заголовки столбцов
            sheet.Cells[3, 1] = "Фамилия";
            sheet.Cells[3, 2] = "Имя";
            sheet.Cells[3, 3] = "Отчество";
            sheet.Cells[3, 4] = "Должность";
            sheet.Cells[3, 5] = "Отдел";
            sheet.Cells[3, 6] = "Год рождения";
            sheet.Cells[3, 7] = "Пол";
            sheet.Cells[3, 8] = "ИНН";
            sheet.Cells[3, 9] = "СНИЛС";
            sheet.Cells[3, 10] = "Номер телефона";
            sheet.Cells[3, 11] = "Почта";
            sheet.Cells[3, 12] = "Дата найма";
            sheet.Cells[3, 13] = "Тип выплаты";
            sheet.Cells[3, 14] = "Дата выплаты";
            sheet.Cells[3, 15] = "Сумма выплаты";

            // Данные
            var payments = ConnectionDB.GetCont().Payments
                .Where(p => p.payment_date >= startDate && p.payment_date <= endDate)
                .OrderBy(p => p.Employee.last_name)
                .ThenBy(p => p.payment_date)
                .ToList();

            // Заполнение данных
            int currentRow = 4;
            foreach (var payment in payments)
            {
                var employee = payment.Employee;
                var position = ConnectionDB.GetCont().Positions.FirstOrDefault(p => p.position_id == employee.position_id);
                var department = position != null ? ConnectionDB.GetCont().Departments.FirstOrDefault(d => d.department_id == position.department_id) : null;
                var gender = ConnectionDB.GetCont().Gndr.FirstOrDefault(g => g.gndr_id == employee.gndr_id);

                sheet.Cells[currentRow, 1] = employee.last_name;
                sheet.Cells[currentRow, 2] = employee.first_name;
                sheet.Cells[currentRow, 3] = employee.middle_name;
                sheet.Cells[currentRow, 4] = position?.position_name ?? "Не указана";
                sheet.Cells[currentRow, 5] = department?.department_name ?? "Не указан";
                sheet.Cells[currentRow, 6] = employee.birthday.Year;
                sheet.Cells[currentRow, 7] = gender?.gndr_name ?? "Не указан";
                sheet.Cells[currentRow, 8] = employee.inn;
                sheet.Cells[currentRow, 9] = employee.snils;
                sheet.Cells[currentRow, 10] = employee.phone_number;
                sheet.Cells[currentRow, 11] = employee.email;
                sheet.Cells[currentRow, 12] = employee.employment_date;
                sheet.Cells[currentRow, 13] = payment.PaymentType.paymenttype_name;
                sheet.Cells[currentRow, 14] = payment.payment_date;
                sheet.Cells[currentRow, 15] = payment.amount;

                currentRow++;
            }

            // Форматирование
            Microsoft.Office.Interop.Excel.Range range = sheet.get_Range("A1", $"O{currentRow - 1}");
            range.Cells.Font.Name = "Times New Roman";
            range.Font.Size = 12;

            // Форматирование заголовка
            var headerRange = sheet.get_Range("A1", "O1");
            headerRange.Merge();
            headerRange.Font.Bold = true;
            headerRange.Font.Size = 14;
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Форматирование заголовков столбцов
            var columnHeaders = sheet.get_Range("A3", "O3");
            columnHeaders.Font.Bold = true;
            columnHeaders.Font.Size = 12;

            // Автоматическая ширина столбцов
            range.Columns.AutoFit();

            // Добавление фильтров TO DO
            
            //var dataRange = sheet.get_Range("A3", $"O{currentRow - 1}");
            //dataRange.AutoFilter();

            // Добавление итогов
            sheet.Cells[currentRow, 1] = "ИТОГО:";
            sheet.Cells[currentRow, 15] = payments.Sum(p => p.amount);
            var totalRange = sheet.get_Range($"A{currentRow}", $"O{currentRow}");
            totalRange.Font.Bold = true;
            totalRange.Font.Size = 12;
        }
    }
}
