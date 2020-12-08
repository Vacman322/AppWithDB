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
        public ManagerPage(MainWindow mainWindow)
        {
            InitializeComponent();
            mv = mainWindow;
            mv.SizeToContent = SizeToContent.Manual;
            mv.WindowState = WindowState.Maximized;
        }
        private void OrderMenuItemClick(object sender, RoutedEventArgs e)
        {
            UserFrame.Navigate(new OrderPage());
        }

        private void OrderEditMenuItemClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ещё не сделано");
        }
    }
}
