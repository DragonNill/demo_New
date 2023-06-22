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
using ООО_Руль.Models.Entity;

namespace ООО_Руль.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ProductInOrderControl.xaml
    /// </summary>
    public partial class ProductInOrderControl : UserControl
    {
        public OrderProduct currentOrderProduct = new OrderProduct();
        Product currentProduct;
        CarPartsContext DbContext;

        public ProductInOrderControl(OrderProduct currentOrderProduct)
        {
            InitializeComponent();

            this.currentOrderProduct = currentOrderProduct;
            DbContext = CarPartsContext.DbContext;

            currentProduct = DbContext.Products.Where(p => p.Id == currentOrderProduct.ProductId).FirstOrDefault();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Name_label.Content = $"Наименование: {currentProduct.Name}";
            Description_label.Content = $"Описание: {currentProduct.Description}";
            Manafacturer_label.Content = $"Производитель: {currentProduct.Manafacturer.Name}";
            Cost_label.Content = $"Стоимость: {currentProduct.Cost}";

            Discount_label.Content = $"Скидка: {currentProduct.Discount}%";

            LoadImage();
        }

        private void LoadImage()
        {
            if (currentProduct.Photo.Length >= 0 && !string.IsNullOrWhiteSpace(currentProduct.Photo))
            {
                Image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory
                    + "//Images//" + currentProduct.Photo, UriKind.Absolute));
            }
            else
            {
                Image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory
                    + "//Images//picture.png", UriKind.Absolute));
            }
        }

        private void Count_textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }

        private void Count_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Convert.ToInt32(Count_textBox.Text) >= currentProduct.Amount)
            {
                Count_textBox.Text = currentProduct.Amount.ToString();
            }
            else if (Convert.ToInt32(Count_textBox.Text) == 0)
            {
                Count_textBox.Text = "1";
            }
        }
    }
}
