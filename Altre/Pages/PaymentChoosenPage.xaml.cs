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
    /// Логика взаимодействия для PaymentChoosenPage.xaml
    /// </summary>
    public partial class PaymentChoosenPage : Page
    {
        bool checkNew;
        Payments payments;
        public PaymentChoosenPage(Payments paymnt)
        {
            InitializeComponent();
            if (paymnt == null)
            {
                paymnt = new Payments();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = payments = paymnt;
            OnLoaded();
        }

        public void OnLoaded() {

        }
    }
}
