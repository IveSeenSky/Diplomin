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
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            UpdateLV();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateLV();
        }

        public void UpdateLV()
        {
            if (Currect.curUser != null)
            {
                Currect.EmployeeList = new List<Employee>();
                foreach (var item in ConnectionDB.context.Employee)
                {
                    var CurPos = ConnectionDB.GetCont().Positions.FirstOrDefault(y => y.position_id == item.position_id);
                    var curDep = ConnectionDB.GetCont().Departments.FirstOrDefault(z => z.department_id == CurPos.department_id);

                    if (curDep == Currect.curDepartment)
                    {
                        Currect.EmployeeList.Add(item);
                    }
                }
                EmplLV.ItemsSource = Currect.EmployeeList;
            } 
            else if (Currect.curUser.user_id == 5)
            {
                EmplLV.ItemsSource = ConnectionDB.GetCont().Employee.ToList();
            }
        }

        private void PaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new PaymentPage((sender as Button).DataContext as Employee));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new EmployeeEditPage(null));
        }

        private void FcBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new PaymentPage(null));
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            Currect.selEmployee = (Employee)(sender as Button).DataContext as Employee;
            Nav.MFrame.Navigate(new EmployeeChoosenPage(Currect.selEmployee));
        }
    }
}
