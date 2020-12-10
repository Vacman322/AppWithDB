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
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        public MainWindow mv;
        public OrderPage OrdPage;
        public EditToolPage EdToolPage;
        public ManagerPage(MainWindow mainWindow)
        {
            InitializeComponent();
            mv = mainWindow;
            mv.SizeToContent = SizeToContent.Manual;
            mv.WindowState = WindowState.Maximized;

            UserData.Db = new DraperyEntities();

            EdToolPage = new EditToolPage();
            ManagerToolFrame.Navigate(EdToolPage);
            OrdPage = HelperClass.ShowPage(ManagerFrame, OrdPage, TableName.order, false);
        }

        private void OrderEditMenuItemClick(object sender, RoutedEventArgs e)
        {
            OrdPage = HelperClass.ShowPage(ManagerFrame, OrdPage, TableName.order, false);
        }
    }
}
