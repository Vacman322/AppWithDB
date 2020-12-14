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
    /// Interaction logic for WarePage.xaml
    /// </summary>
    public partial class WarePage : Page
    {
        public MainWindow mv;
        public ClothPage ClPage;
        public FurniturePage FurPage;
        public ProductPadge ProdPage;
        public WarePage(MainWindow mainWindow)
        {
            InitializeComponent();
            mv = mainWindow;
            UserData.Db = new DraperyEntities();
            mv.SizeToContent = SizeToContent.Manual;
            mv.WindowState = WindowState.Maximized;
            WareToolFrame.Navigate(new AddToolPage());
            ClPage = HelperClass.ShowPage(WareFrame, ClPage, TableName.cloth);
        }

        private void ClothMenuitemClick(object sender, RoutedEventArgs e)
        {
            ClPage = HelperClass.ShowPage(WareFrame, ClPage, TableName.cloth);
        }

        private void FurnitureMenuItemClick(object sender, RoutedEventArgs e)
        {
            FurPage = HelperClass.ShowPage(WareFrame, FurPage, TableName.furniture);
        }

        private void ProductMenuItmeClick(object sender, RoutedEventArgs e)
        {
            ProdPage = HelperClass.ShowPage(WareFrame, ProdPage, TableName.product);
        }

        private void ProdConstructMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (UserData.orderedRecords.Count > 0 && !UserData.orderedRecords.Last().IsFull())
            {
                MessageBox.Show("Добавьте все поля");
                return;
            }

            WareFrame.Navigate(new ProdConstructorPage());
        }
    }
}
