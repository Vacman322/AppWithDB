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
    /// Interaction logic for ProductPadge.xaml
    /// </summary>
    public partial class ProductPadge : Page
    {
        public ProductPadge()
        {
            InitializeComponent();
            var db = new DraperyEntities();
            db.Products.Load();
            DbGrid.ItemsSource = db.Products.Local.ToBindingList();
            db.Dispose();
        }
    }
}
