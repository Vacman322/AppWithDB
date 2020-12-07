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
        }

        private void signButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim();
            var signin = new Signup(login);
            signin.Show();
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim();
            string password = passTextBox.Password;
            string role = "";

            //string connetionStr;
            //SqlConnection cnn;

            //connetionStr = @"Data Source=desktop-v43mklr;Initial Catalog=Drapery;Integrated Security=True";
            //cnn = new SqlConnection(connetionStr);
            try
            {
                using (var cnn = DbInfo.GetSqlConnection())
                {
                    cnn.Open();
                    SqlCommand cmd;
                    SqlDataReader dataReader;
                    string sql;
                    sql = string.Format(@"select [login], [password], [role]
                                  from [User]
                                  where[login] = '{0}' and[password] = '{1}'",
                                          login, password);
                    cmd = new SqlCommand(sql, cnn);
                    dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        MessageBox.Show("Авторизация прошла успешно");
                        role = dataReader.GetString(2).ToLower();
                    }
                    else
                        MessageBox.Show("Авторизация прошла не успешно");

                    cmd.Dispose();
                    //cnn.Close();
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex);
            }

            switch (role)
            {
                case "user":
                    {
                        var uw = new UserWindow();
                        uw.Show();
                        this.Close();
                        break;
                    }
                case "ware":
                    {
                        var ww = new WareWindow();
                        ww.Show();
                        this.Close();
                        break;
                    }
                case "manager":
                    {
                        var mv = new ManagerWindow();
                        mv.Show();
                        this.Close();
                        break;
                    }
                case "admin":
                    {
                        var aw = new AdminWindow();
                        aw.Show();
                        this.Close();
                        break;
                    }
                default:
                    MessageBox.Show("Ошибка: не существует действия для указаной в бд роли");
                    break;
            }
        }
    }
}
