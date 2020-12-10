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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public MainWindow mv;
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            mv = mainWindow;
            passTextBox.Password = "manager";
            loginTextBox.Text = "manager";
        }
        private void signButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim();
            mv.OpenPage(PageName.signUp, login);
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim();
            string password = passTextBox.Password;

            using (var db = new DraperyEntities())
            {
                var users = db.Users;
                var result = users
                    .Where(i => i.login == login && i.password == password);

                if (result.Count() > 0)
                {
                    MessageBox.Show("Авторизация прошла успешно");
                    var roleInDB = result.FirstOrDefault().role;
                    var role = new PageName();
                    if (Enum.TryParse(roleInDB, out role))
                        mv.OpenPage(role,login);
                    else
                        MessageBox.Show("Указанной в бд роли не существует или она указана неверно");
                }
                else
                    MessageBox.Show("Авторизация прошла не успешно");
            }
        }
    }
}
