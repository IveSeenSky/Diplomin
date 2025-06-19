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
using Microsoft.Win32;

namespace Altre.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditPage.xaml
    /// </summary>
    public partial class EmployeeEditPage : Page
    {
        bool checkNew;
        bool result;
        string pathImage;
        static Employee employee;
        public EmployeeEditPage(Employee empl)
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

            GndrCmbx.ItemsSource = ConnectionDB.GetCont().Gndr.ToList();
            PositionCmbx.ItemsSource = ConnectionDB.GetCont().Positions.ToList();
        }

        private void ClearImageBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employee.photo = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }

        private void ImagePathBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
                if (dialog.ShowDialog().GetValueOrDefault(false))
                {
                    pathImage = dialog.FileName;
                }
                ImageBox.Source = new BitmapImage(new Uri(pathImage));
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pathImage != null && pathImage.Trim() != "")
                {
                    employee.photo = File.ReadAllBytes(pathImage);
                }
            }
            catch (Exception ex) 
            {
                employee.photo = null;
                MessageBox.Show(ex.Message);
            }

            if (!checkFormat())
                return;

            if (employee.employee_id == 0) 
            {
                ConnectionDB.GetCont().Employee.Add(employee);
            }
            try
            {
                ConnectionDB.GetCont().SaveChanges();
                if (Nav.MFrame.CanGoBack)
                    Nav.MFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            bool checkFormat()
            {
                //Имя
                if (string.IsNullOrEmpty(firstNameBx.Text))
                {
                    MessageBox.Show("Ошибка сохранения данных: " +
                                    "пустое значение в строке имени",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return false;
                }

                //Отчество
                else if (string.IsNullOrEmpty(middleNameBx.Text))
                {

                    MessageBox.Show("Ошибка сохранения данных: " +
                                    "пустое значение в строке отчества",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return false;
                }

                //Фамилия
                else if (string.IsNullOrEmpty(lastNameBx.Text))
                {
                    MessageBox.Show("Ошибка сохранения данных: " +
                                    "пустое значение в строке имени",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return false;
                }

                //Номер телефона
                else if (string.IsNullOrEmpty(phoneNumberBx.Text) || !IsOnlyDigits(phoneNumberBx.Text))
                {
                    MessageBox.Show("Ошибка сохранения данных: " +
                                    "неверный формат номера телефона" + "\n" + "Допустимы только цифры. Пример: 9009201282",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return false;
                }

                else
                {
                    return true;
                }

            }

            bool IsOnlyDigits(string str)
            {
                foreach (char c in str)
                {
                    if (!char.IsDigit(c))
                        return false;
                }
                return str.Length > 0;
            }
        }
    }
}
