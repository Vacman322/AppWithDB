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
        public OrderPage OrdPage;
        public UserOrderConstructorPage ConstrPage;

        public UserPage(MainWindow mainWindow, string login)
        {
            InitializeComponent();

            UserData.Db = new DraperyEntities();

            ToolFrame.Navigate(new AddToolPage());
            ProdPage = HelperClass.ShowPage(UserFrame, ProdPage, TableName.product);
            ConstrPage = new UserOrderConstructorPage();
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
        
        private void ConstructorMenuItemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(ConstrPage);
        }

        private void UserOrderMenuItemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new UserOrderPage());
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            switch (UserData.CurrentTableName)
            {
                case TableName.product:
                    {
                        if (UserData.orderedRecords.Last().Product == null)
                        {
                            MessageBox.Show("Выберите продукт");
                            return;
                        }
                        var record = UserData.orderedRecords.Last();
                        UserData.Db.Clothes.Load();
                        var bind = UserData.Db.Clothes.Local.Where(c => c.Products.Contains(record.Product)).ToList();
                        ClPage = HelperClass.ShowPage(UserFrame, ClPage, TableName.cloth);
                        ClPage.DbGrid.ItemsSource = bind;          
                    }     
                    break;
                case TableName.cloth:
                    {
                        var record = UserData.orderedRecords.Last();
                        var codes = record.Product.ProductFurnitures.Select(pf => pf.furCode).ToHashSet();
                        UserData.Db.Furnitures.Load();
                        var bind = UserData.Db.Furnitures.Local.Where(f => codes.Contains(f.furCode)).ToList();
                        FurPage = HelperClass.ShowPage(UserFrame, FurPage, TableName.furniture);
                        FurPage.DbGrid.ItemsSource = bind;
                    }
                    break;
                case TableName.furniture:
                        UserFrame.Navigate(ConstrPage);
                    break;
                default:
                    break;
            }
        }
    }
}
