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
    /// Логика взаимодействия для PositionsEditPage.xaml
    /// </summary>
    public partial class PositionsEditPage : Page
    {
        Positions position;
        bool checkNew;
        public PositionsEditPage(Positions pos)
        {
            InitializeComponent();
            if (pos == null)
            {
                pos = new Positions();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = position = pos;
            OnLoaded();
        }

        void OnLoaded()
        {
            DepBx.ItemsSource = ConnectionDB.GetCont().Departments.ToList();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (checkNew) 
                ConnectionDB.GetCont().Positions.Add(position);
            try
            {
                ConnectionDB.GetCont().SaveChanges();
                if (Nav.MFrame.CanGoBack)
                    Nav.MFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
