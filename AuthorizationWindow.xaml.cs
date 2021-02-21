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
using System.Windows.Shapes;
using AutoserviceApp.Data;

namespace AutoserviceApp
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private int countErrorAuths = 0;
        public AuthorizationWindow()
        {
            InitializeComponent();
            txtBoxPassword.Visibility = Visibility.Hidden;
            
        }

        private void checkBoxShowPass_Checked(object sender, RoutedEventArgs e)
        {
            passBoxPassword.Visibility = Visibility.Hidden;
            txtBoxPassword.Visibility = Visibility.Visible;
            txtBlockPassword.Text = "Скрыть пароль";
            txtBoxPassword.Text = passBoxPassword.Password;
        }

        private void checkBoxShowPass_Unchecked(object sender, RoutedEventArgs e)
        {
            passBoxPassword.Visibility = Visibility.Visible;
            txtBoxPassword.Visibility = Visibility.Hidden;
            txtBlockPassword.Text = "Показать пароль";
            passBoxPassword.Password = txtBoxPassword.Text;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            
            AuthHistory history = new AuthHistory();
            int userId = 0;
            foreach (var user in AutoserviceBaseEntities.getContext().User)
            {
                count++;
                if (txtBoxLogin.Text == user.Login && passBoxPassword.Password == user.Password || txtBoxPassword.Text == user.Password)
                {
                    Manager.firstName = user.FirstName;
                    Manager.lastName = user.LastName;
                    foreach(var role in AutoserviceBaseEntities.getContext().Role)
                    {
                        if(user.RoleId == role.Id)
                        {
                            Manager.role = role.Name;
                        }
                    }
                    user.LastEnter = DateTime.Now;
                    userId = user.Id;
                    MessageBox.Show("Вы успешно авторизованы.", "Информация!", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    count = 0;
                    this.Close();
                    break;
                }
                if(txtBoxLogin.Text == user.Login)
                {
                    userId = user.Id;
                }
            }
            if (count != 0)
            {
                MessageBox.Show("Логин или пароль не верны!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                try
                {
                        history.UserId = userId;
                        history.DateTime = DateTime.Now;
                        history.Status = "Blocked";
                        AutoserviceBaseEntities.getContext().AuthHistory.Add(history);
                        AutoserviceBaseEntities.getContext().SaveChanges();
                }
                catch
                {
                    MessageBox.Show("Ошибка записи истории входа в базу данных!\nПроверьте соединение с базой данных.","Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            countErrorAuths++;
            if (countErrorAuths % 3 == 0)
            {
                MessageBox.Show("Превышено количество попыток входа!\nПовторите попытку через 10 секунд.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            try
            {
                if (!string.IsNullOrEmpty(txtBoxLogin.Text) && !string.IsNullOrEmpty(passBoxPassword.Password) && count == 0)
                {
                    history.UserId = userId;
                    history.DateTime = DateTime.Now;
                    history.Status = "Successfull";
                    AutoserviceBaseEntities.getContext().AuthHistory.Add(history);
                    AutoserviceBaseEntities.getContext().SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка связи с базой данной!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
    }
}