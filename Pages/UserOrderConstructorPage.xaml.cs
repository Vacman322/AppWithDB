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
    /// Interaction logic for UserOrderConstructorPage.xaml
    /// </summary>
    public partial class UserOrderConstructorPage : Page
    {
        public UserOrderConstructorPage()
        {
            InitializeComponent();
        }

        private void CreateOrderClick(object sender, RoutedEventArgs e)
        {
            int tmp = 0;
            int prodCount = -1;
            if (int.TryParse(ProdCountTextBox.Text,out tmp))
            {
                prodCount = tmp;
            }
            else
            {
                MessageBox.Show("Введите кол-во продукта");
                return;
            }

            int? managerID = null;
            //User manager = null;
            if (int.TryParse(ManagerIDTextBox.Text, out tmp))
            {
                managerID = tmp;
                //manager = UserData.Db.Users.Where(u => u.userId == managerID).FirstOrDefault();
            }

            var ord = new Ord()
            {
                ordDate = DateTime.Now,
                stageOfExecution = "Новый",
                customerId = UserData.UserInf.userId,
                managerId = managerID,
                ordPrice = 42
            };
            var prodOrd = new OrderedProduct() { Ord =ord, Product = UserData.orderedRecords.Last().Product, ProductQty = prodCount };
            UserData.Db.Ords.Add(ord);
            UserData.Db.OrderedProducts.Add(prodOrd);

            try
            {
                UserData.Db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex);
                return;
            }
            MessageBox.Show("Заказ добавлен!");
            UserData.orderedRecords.Remove(UserData.orderedRecords.Last());
        }
    }
}
