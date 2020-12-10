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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public MainWindow mv;
        public ProductPadge ProdPage;
        public ClothPage ClPage;
        public FurniturePage FurPage;
        public OrderPage OrdPage;
        public EditToolPage AdToolPage;

        public AdminPage(MainWindow mainWindow)
        {            
            InitializeComponent();

            mv = mainWindow;

            UserData.Db = new DraperyEntities();

            mv.SizeToContent = SizeToContent.Manual;
            mv.WindowState = WindowState.Maximized;

            AdToolPage = new EditToolPage();
            ToolFrame.Navigate(AdToolPage);
            ProdPage = HelperClass.ShowPage(UserFrame, ProdPage, TableName.product,false);
        }

        private void ProductMenuItmeClick(object sender, RoutedEventArgs e)
        {
            ProdPage = HelperClass.ShowPage(UserFrame, ProdPage, TableName.product,false);
        }

        private void ClothMenuitemClick(object sender, RoutedEventArgs e)
        {
            ClPage = HelperClass.ShowPage(UserFrame, ClPage,TableName.cloth, false);
        }

        private void FurnitureMenuItemClick(object sender, RoutedEventArgs e)
        {
            FurPage = HelperClass.ShowPage(UserFrame, FurPage,TableName.furniture, false);
        }

        private void OrderMenuItemClick(object sender, RoutedEventArgs e)
        {
            OrdPage = HelperClass.ShowPage(UserFrame,OrdPage,TableName.order, false);
        }
    }
}
