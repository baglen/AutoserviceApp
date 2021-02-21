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
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        private static Service currentService;
        public AddServicePage()
        {
            InitializeComponent();
            currentService = new Service();
            DataContext = currentService;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
         
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(txtBoxTitle.Text))
            {
                stringBuilder.AppendLine("Не введено наименование");
            }
            if (string.IsNullOrEmpty(txtBoxCost.Text))
            {
                stringBuilder.AppendLine("Не введена стоимость");
            }
            if (string.IsNullOrEmpty(txtBoxDuration.Text))
            {
                stringBuilder.AppendLine("Не введена продолжительность");
            }
            if (string.IsNullOrEmpty(txtBoxDiscount.Text))
            {
                stringBuilder.AppendLine("Не введена скидка");
            }
            if (stringBuilder.Length != 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
                
            try
            {
                AutoserviceBaseEntities.getContext().Service.Add(currentService);
                AutoserviceBaseEntities.getContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
                Manager.mainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString());
            }
        }
    }
}
