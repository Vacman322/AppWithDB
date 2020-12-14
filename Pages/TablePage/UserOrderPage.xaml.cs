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
using System.Data.Entity;

namespace AppWithDB.Pages
{
    /// <summary>
    /// Interaction logic for MakeOrderPage.xaml
    /// </summary>
    public partial class UserOrderPage : Page
    {
        public UserOrderPage()
        {
            InitializeComponent();
            //UserData.Db.Ords.Load();
            var bind = UserData.Db.Ords.Where(o => o.customerId == UserData.UserInf.userId).ToList();
            DbGrid.ItemsSource = bind;
        }
    }
}
