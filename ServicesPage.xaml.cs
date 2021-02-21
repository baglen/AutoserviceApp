using AutoserviceApp.Data;
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
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public ServicesPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AutoserviceBaseEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridServices.ItemsSource = AutoserviceBaseEntities.getContext().Service.ToList();
            }
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            if (Manager.role == "booker")
            {
                Manager.mainFrame.Navigate(new AddServicePage());
            }
            else
                MessageBox.Show("Отсутствуют права доступа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
