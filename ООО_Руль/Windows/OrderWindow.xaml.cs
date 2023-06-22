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
using ООО_Руль.UserControls;

namespace ООО_Руль.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        List<OrderProduct> orderProducts = new List<OrderProduct>();
        CarPartsContext DbContext;
        User currentUser;
        Order order = new Order();

        public OrderWindow(List<OrderProduct> orderProducts, User currentUser)
        {
            InitializeComponent();

            this.orderProducts = orderProducts;
            DbContext = CarPartsContext.DbContext;
            this.currentUser = currentUser;

            LoadComboBox();
            LoadLabels();
            LoadListView();
        }

        private void LoadComboBox()
        {
            PickupPoint_comboBox.SelectedIndex = 0;

            foreach (PickupPoint pickupPoint in DbContext.PickupPoints.ToList())
            {
                string dataInComboBox = $"г. {pickupPoint.City.Name}, улица {pickupPoint.Street.Name}, дом {pickupPoint.House}";

                PickupPoint_comboBox.Items.Add(dataInComboBox);
            }
        }

        private void LoadLabels()
        {
            Random rnd = new Random();

            order.PickupCode = rnd.Next(100, 500);
            DateTime dateTime = DateTime.Now;
            DateOnly dateOnly = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            order.OrderDate = dateOnly;
            order.DeliveryDate = dateOnly.AddDays(3);

            PickupCode_label.Content = $"Код для получения: {order.PickupCode}";
            DateOrder_label.Content = $"Дата заказа: {order.OrderDate}";
            DeliveryDate_label.Content = $"Дата получения: {order.DeliveryDate}";

            double totalCost = 0;

            foreach (OrderProduct orderProduct in orderProducts)
            {
                Product product = DbContext.Products.Where(w => w.Id == orderProduct.ProductId).FirstOrDefault();

                totalCost += product.Cost + (product.Cost * (product.Discount / 100));
            }

            TotalCost_label.Content = $"Итоговая сумма: {totalCost} руб.";

        }

        private void LoadListView()
        {
            Products_listView.Items.Clear();

            foreach (OrderProduct orderProduct in orderProducts)
            {
                Products_listView.Items.Add(new ProductInOrderControl(orderProduct)
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LoadListView();
        }

        private void CreateOrder_button_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser != null)
            {
                order.UserId = currentUser.Id;
            }
            else
            {
                order.UserId = null;
            }

            order.PickuppointId = PickupPoint_comboBox.SelectedIndex + 1;
            order.Id = DbContext.Orders.Count() + 1;


            foreach (OrderProduct product in orderProducts)
            {
                product.OrderId = order.Id;

                DbContext.OrderProducts.Add(product);
            }

            DbContext.Orders.Add(order);

            DbContext.SaveChanges();

            MessageBox.Show("Вы успешно создали заказ!\n Талон на данный момент не реализован", 
                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void DeleteFromOrder_menuItem_Click(object sender, RoutedEventArgs e)
        {
            OrderProduct orderProduct = ((ProductInOrderControl)Products_listView.SelectedItem).currentOrderProduct;

            orderProducts.Remove(orderProduct);

            if (orderProducts.Count <= 0)
            {
                MessageBox.Show("Вы удалили все товары из списка!\nОкно Заказа закрывается.", 
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                Close();
            }
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
