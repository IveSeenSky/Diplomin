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
    /// Логика взаимодействия для EmployeeChoosenPage.xaml
    /// </summary>
    public partial class EmployeeChoosenPage : Page
    {
        bool checkNew;
        Employee employee;
        public EmployeeChoosenPage(Employee empl)
        {
            InitializeComponent();
            if (empl == null)
            {
                empl = new Employee();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = employee = empl;
            OnLoaded();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new EmployeeEditPage(Currect.selEmployee));
        }

        void OnLoaded()
        {
            //firstnameTx.Text = employee.first_name;
            //middlenameTx.Text = employee.first_name;
            //lastnameTx.Text = employee.first_name;
            //birthdayTx.Text = employee.first_name;
            //genderTx.Text = employee.first_name;
            //innTx.Text = employee.first_name;
            //snilsTx.Text = employee.first_name;
            //phonenumberTx.Text = employee.first_name;
            //emailTx.Text = employee.first_name;
            //employmentdateTx.Text = employee.first_name;

            if (employee.termination_date == null)
                termonationdateBx.Visibility = Visibility.Collapsed;

            if (employee.termination_reason == null)
                terminationreasonBx.Visibility = Visibility.Collapsed;

            var position = ConnectionDB.GetCont().Positions.FirstOrDefault(x => x.position_id == employee.position_id);
            var depart = ConnectionDB.GetCont().Departments.FirstOrDefault(x => x.department_id == position.department_id);

            positionTx.Text = position.position_name;
            departTx.Text = depart.department_name;
        }
    }
}
