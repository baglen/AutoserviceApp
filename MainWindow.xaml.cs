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

namespace AutoserviceApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.mainFrame = MainFrame;
            Manager.mainFrame.Navigate(new UserInfoPage());
            if(Manager.role == "admin")
            {
                btnServices.Visibility = Visibility.Hidden;
                btnClients.Visibility = Visibility.Hidden;
                btnOrders.Visibility = Visibility.Hidden;
                btnProducts.Visibility = Visibility.Hidden;
                btnReport.Visibility = Visibility.Hidden;
            }
            if(Manager.role == "manager")
            {
                btnUsers.Visibility = Visibility.Hidden;
            }
            if(Manager.role == "booker")
            {
                btnUsers.Visibility = Visibility.Hidden;
            }
          
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.GoBack();
        }

        private void btcLogout_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            
            Manager.mainFrame.Navigate(new UsersPage());
        }

        private void btnServices_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new ServicesPage());
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new ClientsPage());
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new ClientServicesPage());
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new ProductsPage());
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new ReportsPage());
        }
    }
}
