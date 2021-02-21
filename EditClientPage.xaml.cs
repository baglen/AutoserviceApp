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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class EditClientPage : Page
    {
        private Client currentClient;
        public EditClientPage(Client client)
        {
            InitializeComponent();
            currentClient = new Client();
            if (client != null)
                currentClient = client;
            DataContext = currentClient;
            comboBoxGender.ItemsSource = AutoserviceBaseEntities.getContext().Gender.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(currentClient.FirstName))
            {
                stringBuilder.AppendLine("Не введено имя");
            }
            if (string.IsNullOrEmpty(currentClient.LastName))
            {
                stringBuilder.AppendLine("Не введена фамилия");
            }
            if (string.IsNullOrEmpty(currentClient.Patronymic))
            {
                stringBuilder.AppendLine("Не введено отчество");
            }
            if (datePickerBirthday.SelectedDate == null)
            {
                stringBuilder.AppendLine("Не выбрана дата рождения");
            }
            if (datePickerRegDate.SelectedDate == null)
            {
                stringBuilder.AppendLine("Не выбрана дата регистрации");
            }
            if (string.IsNullOrEmpty(currentClient.Email))
            {
                stringBuilder.AppendLine("Не введено отчество");
            }
            if (string.IsNullOrEmpty(currentClient.Phone))
            {
                stringBuilder.AppendLine("Не введен телефон");
            }
            if (comboBoxGender.SelectedItem == null)
            {
                stringBuilder.AppendLine("Не введен пол");
            }
            if (stringBuilder.Length != 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                AutoserviceBaseEntities.getContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString());
            }
        }
    }
}
