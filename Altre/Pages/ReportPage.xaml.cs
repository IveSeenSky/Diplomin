using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Altre.AppData;

namespace Altre.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
            // Set default dates to current month
            StartDatePicker.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDatePicker.SelectedDate = DateTime.Now;
        }

        private void GenerateReportBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!StartDatePicker.SelectedDate.HasValue || !EndDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, выберите период для отчета", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var startDate = StartDatePicker.SelectedDate.Value;
            var endDate = EndDatePicker.SelectedDate.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Дата начала не может быть позже даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ReportConstructor.getReport(startDate, endDate);
        }
    }
}
