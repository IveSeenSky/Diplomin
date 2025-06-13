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
using Altre.Frames;

namespace Altre.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectionDB.GetCont().Users.Any(x => x.username == LoginBx.Text && x.password == PasswordBx.Password))
            {
                Currect.curUser = ConnectionDB.GetCont().Users.FirstOrDefault(x => x.username == LoginBx.Text && x.password == PasswordBx.Password);
                Nav.Fullframe.Visibility = Visibility.Collapsed;
                Nav.MFrame.Navigate(new EmployeesPage());
                Nav.TFrame.Navigate(new MenuFrame());
            }
        }
    }
}
