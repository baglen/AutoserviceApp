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
using AutoserviceApp.Data;

namespace AutoserviceApp
{
    /// <summary>
    /// Логика взаимодействия для ClientServiceAddPage.xaml
    /// </summary>
    public partial class ClientServiceAddPage : Page
    {
        private static ClientService currentClientService;
        public ClientServiceAddPage()
        {
            InitializeComponent();
            currentClientService = new ClientService();
            DataContext = currentClientService;
            comboBoxClient.ItemsSource = AutoserviceBaseEntities.getContext().Client.ToList();
            comboBoxService.ItemsSource = AutoserviceBaseEntities.getContext().Service.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (comboBoxClient.SelectedItem == null)
            {
                stringBuilder.AppendLine("Не выбран клиент");
            }
            if (comboBoxService.SelectedItem == null)
            {
                stringBuilder.AppendLine("Не выбрана услуга");
            }
            if (datePickerBoxStart.ToString() == null)
            {
                stringBuilder.AppendLine("Не выбрана дата начала");
            }
            if (stringBuilder.Length != 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } 
            try
            {
                currentClientService.StartTime = (DateTime)datePickerBoxStart.SelectedDate;
                AutoserviceBaseEntities.getContext().ClientService.Add(currentClientService);
                AutoserviceBaseEntities.getContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString());
                return;
            }
        }
    }
}
