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

namespace Altre.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            OnLoaded();
        }

        void OnLoaded()
        {
            UsersLV.ItemsSource = ConnectionDB.GetCont().PermConct.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new Pages.AddEdit.UserEditPage((sender as Button).DataContext as PermConct));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new Pages.AddEdit.UserEditPage(null));
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var delProd = UsersLV.SelectedItems.Cast<PermConct>().ToList();
                foreach (var item in delProd)
                {
                    if (ConnectionDB.GetCont().Users.Any(x => x.user_id == item.user_id) ||
                        ConnectionDB.GetCont().Employee.Any(x => x.employee_id == item.employee_id))
                    {
                        MessageBox.Show("Данные используются в другой таблице");
                        return;
                    }
                }
                if (MessageBox.Show("Удалить " + delProd.Count + " записей", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ConnectionDB.GetCont().PermConct.RemoveRange(delProd);
                    ConnectionDB.GetCont().SaveChanges();
                    MessageBox.Show("Данные удалены");
                }
            }
            catch { }
        }

        private void UpdBtn_Click(object sender, RoutedEventArgs e)
        {
            OnLoaded();
        }
    }
}
