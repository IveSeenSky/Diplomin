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
using Altre.Pages.AddEdit;

namespace Altre.Pages
{
    /// <summary>
    /// Логика взаимодействия для PositionsPage.xaml
    /// </summary>
    public partial class PositionsPage : Page
    {
        public PositionsPage()
        {
            InitializeComponent();
            OnLoaded();
        }
        
        void OnLoaded()
        {
            PosLV.ItemsSource = ConnectionDB.GetCont().Positions.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new PositionsEditPage((sender as Button).DataContext as Positions));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MFrame.Navigate(new PositionsEditPage(null));
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var delProd = PosLV.SelectedItems.Cast<Positions>().ToList();
                foreach (var item in delProd)
                {
                    if (ConnectionDB.GetCont().Employee.Any(x => x.position_id == item.position_id))
                    {
                        MessageBox.Show("Данные используются в другой таблице");
                        return;
                    }
                }
                if (MessageBox.Show("Удалить " + delProd.Count + " записей", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ConnectionDB.GetCont().Positions.RemoveRange(delProd);
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
