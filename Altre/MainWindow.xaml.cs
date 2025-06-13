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
            
            //ImgImport(@"..\..\Photos");
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Nav.MFrame.CanGoBack) 
                Nav.MFrame.GoBack();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при импорте изображений: {ex.Message}");
            }
        }
    }
}
