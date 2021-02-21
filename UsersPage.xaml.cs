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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new EditUserPage((sender as Button).DataContext as User));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AutoserviceBaseEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridUsers.ItemsSource = AutoserviceBaseEntities.getContext().User.ToList();
            }
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            Manager.mainFrame.Navigate(new HistoryPage());
        }
    }
}
