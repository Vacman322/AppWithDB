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
using System.Data.Entity;

namespace AppWithDB.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public MainWindow mv;
        public ProductPadge ProdPage;
        public ClothPage ClPage;
        public FurniturePage FurPage;
        public UserOrderPage OrdPage;
        public UserBasketPage Basket;

        public UserPage(MainWindow mainWindow, string login)
        {
            InitializeComponent();

            UserData.Db = new DraperyEntities();

            ProdPage = HelperClass.ShowPage(UserFrame, ProdPage, TableName.product);
            OrdPage = new UserOrderPage();
            mv = mainWindow;
            mv.SizeToContent = SizeToContent.Manual;
            mv.WindowState = WindowState.Maximized;
        }
        private void ProductMenuItmeClick(object sender, RoutedEventArgs e)
        {
            ProdPage = HelperClass.ShowPage(UserFrame, ProdPage, TableName.product);
        }

        private void ClothMenuitemClick(object sender, RoutedEventArgs e)
        {
            ClPage = HelperClass.ShowPage(UserFrame, ClPage, TableName.cloth);
        }

        private void FurnitureMenuItemClick(object sender, RoutedEventArgs e)
        {
            FurPage = HelperClass.ShowPage(UserFrame, FurPage, TableName.furniture);
        }
        

        private void UserOrderMenuItemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(OrdPage);
        }

        private void BasketMenuItemClick(object sender, RoutedEventArgs e)
        {
            Basket = new UserBasketPage();
            UserFrame.Navigate(Basket);
        }
    }
}
