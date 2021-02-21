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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            searchInTxtBox();
            string[] sortArray = { "По фамилии", "По имени", "По отчеству" };
            comboBoxSort.ItemsSource = sortArray;
        }
        public void searchInTxtBox()
        {
            List<Client> currentClient = AutoserviceBaseEntities.getContext().Client.ToList();
            currentClient = currentClient.Where(p => p.FirstName.ToLower().Contains(txtBoxSearch.Text.ToLower()) || p.LastName.ToLower().Contains(txtBoxSearch.Text.ToLower()) || p.Patronymic.ToLower().Contains(txtBoxSearch.Text.ToLower()) || p.Email.ToLower().Contains(txtBoxSearch.Text.ToLower()) ||  p.Phone.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
            ListViewClient.ItemsSource = currentClient.ToList();
        }
        
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AutoserviceBaseEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListViewClient.ItemsSource = AutoserviceBaseEntities.getContext().Client.ToList();
            }
        }
        private void ListViewClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Client client = (Client)ListViewClient.SelectedItem;
            Manager.mainFrame.Navigate(new EditClientPage(client));
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchInTxtBox();
        }

        private void comboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBoxSort.SelectedIndex == 0)
                ListViewClient.ItemsSource = AutoserviceBaseEntities.getContext().Client.OrderBy(p => p.LastName).ToList();
            if (comboBoxSort.SelectedIndex == 1)
                ListViewClient.ItemsSource = AutoserviceBaseEntities.getContext().Client.OrderBy(p => p.FirstName).ToList();
            if (comboBoxSort.SelectedIndex == 2)
                ListViewClient.ItemsSource = AutoserviceBaseEntities.getContext().Client.OrderBy(p => p.Patronymic).ToList();
        }
    }
}
