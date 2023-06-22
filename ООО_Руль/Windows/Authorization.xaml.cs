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
using ООО_Руль.Windows;
using ООО_Руль.Models.Entity;

namespace ООО_Руль
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        CarPartsContext DbContext;

        public Authorization()
        {
            InitializeComponent();

            DbContext = CarPartsContext.DbContext;
        }

        private void Enter_button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(Login_textBox.Text) && !string.IsNullOrWhiteSpace(Password_passwordBox.Password))
            {
                User user = DbContext.Users.Where(w => w.UserLogin == Login_textBox.Text 
                && w.UserPassword == Password_passwordBox.Password).FirstOrDefault();

                if (user != null)
                {
                    ProductsWindow nextWindows = new ProductsWindow(user);

                    MessageBox.Show($"Вы успешно авторизовались как {user.UserName} {user.UserSurname} {user.UserPatronomyc}!", 
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                    nextWindows.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EnterAsGues_button_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow nextWindows = new ProductsWindow(null);

            MessageBox.Show($"Вы успешно авторизовались как Гость!",
                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            nextWindows.Show();
            Close();
        }
    }
}
