using System;
using System.Collections.Generic;
using System.IO;
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
            if (Currect.curUser != null && 
                Currect.curUser.perms_id != 0)
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
            else if (Currect.curUser.perms_id == 0)
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

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var delProd = EmplLV.SelectedItems.Cast<Employee>().ToList();
                foreach (var item in delProd)
                {
                    if (ConnectionDB.GetCont().PermConct.Any(x => x.employee_id == item.employee_id) ||
                        ConnectionDB.GetCont().Payments.Any(x => x.employee_id == item.employee_id)) 
                    {
                        MessageBox.Show("Данные используются в другой таблице");
                        return;
                    }
                }
                if (MessageBox.Show("Удалить " + delProd.Count + " записей", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ConnectionDB.GetCont().Employee.RemoveRange(delProd);
                    ConnectionDB.GetCont().SaveChanges();
                    MessageBox.Show("Данные удалены");
                }
            }
            catch { }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateLV();
            ImgImport(@"..\..\Photos");
        }

        public static void ImgImport(string st)
        {
            try
            {
                var context = ConnectionDB.GetCont();
                var images = Directory.GetFiles(System.IO.Path.GetFullPath(st));
                var employees = context.Employee.ToList();

                foreach (var empl in employees)
                {
                    if (string.IsNullOrEmpty(empl.photopath))
                        continue;

                    var matchingImage = images.FirstOrDefault(x => x.Contains(empl.photopath));
                    if (matchingImage != null)
                    {
                        try
                        {
                            empl.photo = File.ReadAllBytes(matchingImage);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при чтении файла для сотрудника {empl.last_name}: {ex.Message}");
                        }
                    }
                }
                context.SaveChanges();
            } catch { }
        }

        private void SearchBx_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Currect.curUser.perms_id == 0)
                {
                    EmplLV.ItemsSource = ConnectionDB.GetCont().Employee.Where(x => x.first_name.Contains(SearchBx.Text) ||
                                                                                    x.middle_name.Contains(SearchBx.Text) ||
                                                                                    x.last_name.Contains(SearchBx.Text)).ToList();
                }
                else
                {
                    EmplLV.ItemsSource = Currect.EmployeeList.Where(x => x.first_name.Contains(SearchBx.Text) ||
                                                                         x.middle_name.Contains(SearchBx.Text) ||
                                                                         x.last_name.Contains(SearchBx.Text)).ToList();
                }
            }
            catch
            {

            }
        }
    }
}
