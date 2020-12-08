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
        public WarePage(MainWindow mainWindow)
        {
            InitializeComponent();
            mv = mainWindow;
            mv.SizeToContent = SizeToContent.Manual;
            mv.WindowState = WindowState.Maximized;
        }

        private void ClothMenuitemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new ClothPage());
        }

        private void FurnitureMenuItemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new FurniturePage());
        }
    }
}
