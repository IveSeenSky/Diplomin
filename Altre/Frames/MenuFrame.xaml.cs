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
using Altre.Pages;

namespace Altre.Frames
{
    /// <summary>
    /// Логика взаимодействия для MenuFrame.xaml
    /// </summary>
    public partial class MenuFrame : Page
    {
        public MenuFrame()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void RprtBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new ReportPage());
        }

        private void EmplBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new EmployeesPage());
        }

        private void PmntBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new PaymentPage(null));
        }
    }
}
