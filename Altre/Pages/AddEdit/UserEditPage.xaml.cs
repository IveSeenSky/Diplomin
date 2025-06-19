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

namespace Altre.Pages.AddEdit
{
    /// <summary>
    /// Логика взаимодействия для UserEditPage.xaml
    /// </summary>
    public partial class UserEditPage : Page
    {
        PermConct PermConcts;
        bool checkNew;
        public UserEditPage(PermConct permConct)
        {
            InitializeComponent();
            if (permConct == null)
            {
                permConct = new PermConct();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = PermConcts = permConct;
            OnLoaded();
        }

        void OnLoaded()
        {
            EmplsCmbx.ItemsSource = ConnectionDB.GetCont().Employee.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!ConnectionDB.GetCont().Users.Any(x => x.username.Equals(LoginBx.Text)))
            {
                Users users = new Users();
                users.username = LoginBx.Text;
                users.password = PassBx.Text;
                users.perms_id = FindPerms();

                try
                {
                    ConnectionDB.GetCont().Users.Add(users);
                    ConnectionDB.GetCont().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            if (checkNew)
            {
                ConnectionDB.GetCont().PermConct.Add(PermConcts);
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
        }

        int FindPerms()
        {
            int count = -1;
            foreach (Users item in ConnectionDB.GetCont().Users.ToList())
            {
                if (count < item.perms_id)
                {
                    count = item.perms_id;
                }
            }
            return count++;
        }
    }
}
