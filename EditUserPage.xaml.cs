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
    /// Логика взаимодействия для AddEditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        private User currentUser;
        public EditUserPage(User user)
        {
            InitializeComponent();
            currentUser = new User();
            if (user != null)
                currentUser = user;
            DataContext = currentUser;
            comboBoxRole.ItemsSource = AutoserviceBaseEntities.getContext().Role.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(currentUser.FirstName))
            {
                stringBuilder.AppendLine("Не введено имя");
            }
            if (string.IsNullOrEmpty(currentUser.LastName))
            {
                stringBuilder.AppendLine("Не введена фамилия");
            }
            if (string.IsNullOrEmpty(currentUser.Login))
            {
                stringBuilder.AppendLine("Не введен логин");
            }
            if (comboBoxRole.SelectedItem == null)
            {
                stringBuilder.AppendLine("Не введена роль");
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
