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
using System.Windows.Shapes;

namespace AppWithDB
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Signup : Window
    {
        public Signup(string login)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(login))
                loginTextBox.Text = login;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim();
            string pass = passTextBox.Password;
            string pass2 = passTextBox2.Password;
            string name = nameTextBox.Text.Trim();
            if (!string.Equals(pass, pass2))
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            try
            {
                using (var cnn = ConnectionToDb.GetSqlConnection())
                {
                    cnn.Open();
                    SqlCommand command;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    string sql = string.Format(@"insert into [user] ([login],[password],[role],UserName) 
                                         values('{0}','{1}','user','{2}')", login, pass, name);
                    command = new SqlCommand(sql, cnn);
                    adapter.InsertCommand = command;
                    adapter.InsertCommand.ExecuteNonQuery();

                    command.Dispose();
                }
                MessageBox.Show("Вы зарегистрированы");
                var mv = new MainWindow();
                mv.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex);
            }        
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
            var mv = new MainWindow();
            mv.Show();
            this.Close();
        }
    }
}
