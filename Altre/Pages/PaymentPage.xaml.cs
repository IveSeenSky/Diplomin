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
    /// Логика взаимодействия для PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        public PaymentPage(Employee empl)
        {
            InitializeComponent();
            loadLV(empl);
        }

        void loadLV(Employee empl)
        {
            if (empl == null)
                if (Currect.curUser.user_id == 5)
                    pmntLV.ItemsSource = ConnectionDB.GetCont().Payments.ToList();
                else if (Currect.EmployeeList != null)
                    foreach(var item in Currect.EmployeeList)
                    {
                        var sPaymnt = ConnectionDB.GetCont().Payments.Where(x => x.employee_id == item.employee_id);
                        foreach (var item2 in sPaymnt)
                            pmntLV.Items.Add(item2);
                    }
                else Nav.MFrame.Navigate(new EmployeesPage());
            else
            {
                pmntLV.ItemsSource = ConnectionDB.GetCont().Payments.Where(x => x.employee_id == empl.employee_id).ToList();
            }
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new PaymentChoosenPage((sender as Button).DataContext as Payments));
        }
    }
}
