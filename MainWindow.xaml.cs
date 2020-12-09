using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using AppWithDB.Pages;

namespace AppWithDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OpenPage(PageName.login);
        }

        public void OpenPage(PageName pageName, string login = "")
        {
            switch (pageName)
            {
                case PageName.login:
                    MainFrame.Navigate(new LoginPage(this));
                    break;
                case PageName.signUp:
                    MainFrame.Navigate(new SingUpPage(login, this));
                    break;
                case PageName.user:
                    MainFrame.Navigate(new UserPage(this,login));
                    break;
                case PageName.admin:
                    MainFrame.Navigate(new AdminPage(this));
                    break;
                case PageName.manager:
                    MainFrame.Navigate(new ManagerPage(this));
                    break;
                case PageName.ware:
                    MainFrame.Navigate(new WarePage(this));
                    break;
                default:
                    MessageBox.Show("Ошибка: Страници с таким названием не сделана(");
                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (UserData.Db != null) UserData.Db.Dispose();
        }
    }
}
