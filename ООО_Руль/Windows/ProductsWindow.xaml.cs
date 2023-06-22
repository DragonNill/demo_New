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
using ООО_Руль.Models.Entity;
using ООО_Руль.Windows;
using ООО_Руль.UserControls;


namespace ООО_Руль.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        User currentUser;
        CarPartsContext DbContext;
        List<OrderProduct> orderProducts = new List<OrderProduct>();

        public ProductsWindow(User currentUser)
        {
            InitializeComponent();

            DbContext = CarPartsContext.DbContext;
            this.currentUser = currentUser;

            LoadListView();
        }

        private void LoadListView()
        {
            Products_listView.Items.Clear();

            List<Product> products = DbContext.Products.ToList();

            if (!string.IsNullOrWhiteSpace(Search_textBox.Text))
            {
                products = products.Where(w => w.Name.ToLower().Contains(Search_textBox.Text.ToLower()) 
                || w.Description.ToLower().Contains(Search_textBox.Text.ToLower()) 
                || w.Amount.ToString().ToLower().Contains(Search_textBox.Text.ToLower())
                || w.Cost.ToString().ToLower().Contains(Search_textBox.Text.ToLower())
                || w.Discount.ToString().ToLower().Contains(Search_textBox.Text.ToLower())
                || w.Manafacturer.Name.ToLower().Contains(Search_textBox.Text.ToLower())).ToList();
            }

            foreach (Product product in products)
            {
                Products_listView.Items.Add(new ProductControl(product)
                {
                    Width = GetWidth()
                });
            }
        }

        private double GetWidth()
        {
            if (WindowState == WindowState.Maximized)
            {
                return RenderSize.Width - 55;
            }
            else
            {
                return Width - 55;
            }
        }

        private void AddToOrder_menuItem_Click(object sender, RoutedEventArgs e)
        {
            Product product = ((ProductControl)Products_listView.SelectedItem).currentProduct;

            OrderProduct orderProduct = orderProducts.Find(f => f.ProductId == product.Id);

            if (orderProduct == null)
            {
                orderProduct = new OrderProduct();

                orderProduct.ProductId = product.Id;
                orderProduct.Count = 1;

                orderProducts.Add(orderProduct);

                GoToOrder_button.Visibility = Visibility.Visible;
            }
            else
            {
                return;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Authorization nextWindow = new Authorization();
            nextWindow.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LoadListView();
        }

        private void Search_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadListView();
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GoToOrder_button_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow nextWindow = new OrderWindow(orderProducts, currentUser);

            nextWindow.ShowDialog();
        }
    }
}
