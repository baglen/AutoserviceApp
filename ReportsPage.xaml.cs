using Microsoft.Win32;
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
using System.IO;

namespace AutoserviceApp
{
    /// <summary>
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(radioService.IsChecked == true)
            { 
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "csv files (.csv)|.csv";
                    saveFileDialog.ShowDialog();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Наименование, Цена, Длительность, Описание, Скидка ");
                    sb.AppendLine();
                    foreach (var service in AutoserviceBaseEntities.getContext().Service)
                    {
                        sb.Append(service.Title+" ");
                        sb.Append(service.Cost + " ");
                        sb.Append(service.DurationInSeconds + " ");
                        sb.Append(service.Description + " ");
                        sb.Append(service.Discount);
                        sb.AppendLine();
                    }
                    File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            if (radioProduct.IsChecked == true)
            {
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "csv files (.csv)|.csv";
                    saveFileDialog.ShowDialog();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Наименование, Цена, Описание, Статус, Производитель ");
                    sb.AppendLine();
                    foreach (var product in AutoserviceBaseEntities.getContext().Product)
                    {
                        sb.Append(product.Title + " ");
                        sb.Append(product.Cost + " ");
                        sb.Append(product.Description + " ");
                        sb.Append(product.IsActive + " ");
                        sb.Append(product.Manufacturer.Name);
                        sb.AppendLine();
                    }
                    File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void radioService_Click(object sender, RoutedEventArgs e)
        {
            DGridServices.Visibility = Visibility.Visible;
            DGridProducts.Visibility = Visibility.Collapsed;
            txtTitle.Text = "Услуги";
        }

        private void radioProduct_Click(object sender, RoutedEventArgs e)
        {
            DGridServices.Visibility = Visibility.Collapsed;
            DGridProducts.Visibility = Visibility.Visible;
            txtTitle.Text = "Товары";
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AutoserviceBaseEntities.getContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridProducts.ItemsSource = AutoserviceBaseEntities.getContext().Product.ToList();
                DGridServices.ItemsSource = AutoserviceBaseEntities.getContext().Service.ToList();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Печать", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
