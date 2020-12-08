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

namespace AppWithDB.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        string login;
        public MainWindow mv;
        public UserPage(MainWindow mainWindow, string login)
        {
            InitializeComponent();
            this.login = login;
            mv = mainWindow;
            mv.SizeToContent = SizeToContent.Manual;
            mv.WindowState = WindowState.Maximized;
        }
        private void ProductMenuItmeClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new ProductPadge());
        }

        private void ClothMenuitemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new ClothPage());
        }

        private void FurnitureMenuItemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new FurniturePage());
        }

        private void OrderMenuItemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new OrderPage());
        }
        
        private void ConstructorMenuItemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new ConstructorPage());
        }

        private void UserOrderMenuItemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new UserOrderPage());
        }
    }
}
