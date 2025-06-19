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
using Altre.Pages;

namespace Altre
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Nav.MFrame = MainFrame;
            Nav.TFrame = TipeFrame;
            Nav.Fullframe = FullFrame;
            Nav.Fullframe.Navigate(new LoginPage());
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Nav.MFrame.CanGoBack) 
                Nav.MFrame.GoBack();
        }
    }
}
