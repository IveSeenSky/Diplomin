using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Altre.AppData
{
    internal class Reports
    {
        public static void Employees(DateTime? startDate = null, DateTime? endDate = null)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application()
            {
                Visible = true,
                SheetsInNewWorkbook = 1
            };
            Microsoft.Office.Interop.Excel.Workbook work = app.Workbooks.Add(Type.Missing);
            app.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)app.Worksheets.get_Item(1);
            sheet.Name = "Отчет по сотрудникам";

            // Заголовки столбцов
            sheet.Cells[1, 1] = "Должность";
            sheet.Cells[1, 2] = "Фамилия";
            sheet.Cells[1, 3] = "Имя";
            sheet.Cells[1, 4] = "Отчество";
            sheet.Cells[1, 5] = "Отдел";
            sheet.Cells[1, 6] = "Год рождения";
            sheet.Cells[1, 7] = "Пол";
            sheet.Cells[1, 8] = "ИНН";
            sheet.Cells[1, 9] = "СНИЛС";
            sheet.Cells[1, 10] = "Номер телефона";
            sheet.Cells[1, 11] = "Почта";
            sheet.Cells[1, 12] = "Дата найма";
            sheet.Cells[1, 13] = "Сумма выплат";
            sheet.Cells[1, 14] = "Количество выплат";

            // Получаем данные с фильтрацией по дате
            var employe = ConnectionDB.GetCont().Employee
                .Where(e => (!startDate.HasValue || e.employment_date >= startDate) &&
                           (!endDate.HasValue || e.employment_date <= endDate))
                .OrderBy(e => e.Positions.position_name)
                .ThenBy(e => e.last_name)
                .ToList();

            List<Employee> employees = new List<Employee>();

            if (Currect.curUser.perms_id != 0)
            {
                foreach (var employee in employe)
                {
                    var CurPos = ConnectionDB.GetCont().Positions.FirstOrDefault(y => y.position_id == employee.position_id);
                    var curDep = ConnectionDB.GetCont().Departments.FirstOrDefault(z => z.department_id == CurPos.department_id);
                    employees.Add(employee);
                }
            }
            else
            {
                employees = employe.ToList();
            }

            // Заполнение данных
            var currentRow = 2;
            foreach (var employee in employees)
            {
                var position = ConnectionDB.GetCont().Positions.FirstOrDefault(y => y.position_id == employee.employee_id);
                var gndr = ConnectionDB.GetCont().Gndr.FirstOrDefault(z => z.gndr_id == employee.gndr_id);
                var division = ConnectionDB.GetCont().Departments.FirstOrDefault(x => x.department_id == position.department_id);

                // Получаем информацию о выплатах
                var payments = ConnectionDB.GetCont().Payments
                    .Where(p => p.employee_id == employee.employee_id)
                    .ToList();
                var totalPayments = payments.Sum(p => p.amount);
                var paymentCount = payments.Count;

                sheet.Cells[currentRow, 1] = position?.position_name ?? "Не указана";
                sheet.Cells[currentRow, 2] = employee.last_name;
                sheet.Cells[currentRow, 3] = employee.first_name;
                sheet.Cells[currentRow, 4] = employee.last_name;
                sheet.Cells[currentRow, 5] = division?.department_name ?? "Не указан";
                sheet.Cells[currentRow, 6] = employee.birthday;
                sheet.Cells[currentRow, 7] = gndr?.gndr_name ?? "Не указан";
                sheet.Cells[currentRow, 8] = employee.inn;
                sheet.Cells[currentRow, 9] = employee.snils;
                sheet.Cells[currentRow, 10] = employee.phone_number;
                sheet.Cells[currentRow, 11] = employee.email;
                sheet.Cells[currentRow, 12] = employee.employment_date;
                sheet.Cells[currentRow, 13] = totalPayments;
                sheet.Cells[currentRow, 14] = paymentCount;

                currentRow++;
            }

            // Форматирование
            Microsoft.Office.Interop.Excel.Range rang = sheet.get_Range("A1", "N" + (currentRow - 1).ToString());
            rang.Cells.Font.Name = "Times New Roman";
            rang.Font.Size = 12;
            
            // Форматирование заголовков
            var headerRange = sheet.get_Range("A1", "N1");
            headerRange.Font.Bold = true;
            headerRange.Font.Size = 14;
            
            // Автоматическая ширина столбцов
            rang.Columns.AutoFit();

            // Добавление фильтров
            rang.AutoFilter();

            // Группировка по должностям
            sheet.Outline.SummaryRow = XlSummaryRow.xlSummaryAbove;
            sheet.Outline.SummaryColumn = XlSummaryColumn.xlSummaryOnLeft;
            
            // Добавление итогов
            sheet.Cells[currentRow, 1] = "ИТОГО:";
            sheet.Cells[currentRow, 13] = employees.Sum(e => 
                ConnectionDB.GetCont().Payments
                    .Where(p => p.employee_id == e.employee_id)
                    .Sum(p => p.amount));
            sheet.Cells[currentRow, 14] = employees.Sum(e => 
                ConnectionDB.GetCont().Payments
                    .Count(p => p.employee_id == e.employee_id));

            // Форматирование итоговой строки
            var totalRange = sheet.get_Range($"A{currentRow}", $"N{currentRow}");
            totalRange.Font.Bold = true;
            totalRange.Font.Size = 14;
        }
    }
}
