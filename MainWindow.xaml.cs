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

            //string connetionStr;
            //SqlConnection cnn;

            //connetionStr = @"Data Source=desktop-v43mklr;Initial Catalog=Drapery;Integrated Security=True";
            //cnn = new SqlConnection(connetionStr);
            try
            {
                using (var cnn = ConnectionToDb.GetSqlConnection())
                {
                    cnn.Open();
                    SqlCommand cmd;
                    SqlDataReader dataReader;
                    string sql;
                    sql = string.Format(@"select [login], [password]
                                  from[User]
                                  where[login] = '{0}' and[password] = '{1}'",
                                          login, password);
                    cmd = new SqlCommand(sql, cnn);
                    dataReader = cmd.ExecuteReader();

                    if (dataReader.HasRows)
                        MessageBox.Show("Авторизация прошла успешно");
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
        }
    }
}
