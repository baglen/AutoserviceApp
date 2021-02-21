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
    /// Логика взаимодействия для UserInfoPage.xaml
    /// </summary>
    public partial class UserInfoPage : Page
    {
        public UserInfoPage()
        {
            InitializeComponent();
            txtBlockFirstName.Text += Manager.firstName;
            txtBlockLastName.Text += Manager.lastName;
            txtBlockRole.Text += Manager.role;
            roleImage.Source = new BitmapImage(new Uri($"Resources/{Manager.role}.png", UriKind.Relative));
        }
    }
}
