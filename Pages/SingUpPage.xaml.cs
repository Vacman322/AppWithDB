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
    /// Interaction logic for SingUpPage.xaml
    /// </summary>
    public partial class SingUpPage : Page
    {
        public MainWindow mv;
        public SingUpPage(string login,MainWindow mv)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(login))
                loginTextBox.Text = login;

            this.mv = mv;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string inputlogin = loginTextBox.Text.Trim();
            string pass = passTextBox.Password;
            string pass2 = passTextBox2.Password;
            string name = nameTextBox.Text.Trim();

            if (!string.Equals(pass, pass2))
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            using (var db = new DraperyEntities())
            {
                db.Users.Add(new User() { login = inputlogin, password = pass, role = "user", UserName = name });
                db.SaveChanges();
            }

            MessageBox.Show("Вы зарегистрированы");
            mv.OpenPage(PageName.login);
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            loginTextBox.Clear();
            passTextBox.Clear();
            passTextBox2.Clear();
            nameTextBox.Clear();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            mv.OpenPage(PageName.login);
        }
    }
}
