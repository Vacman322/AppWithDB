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
    /// Interaction logic for ClothPage.xaml
    /// </summary>
    public partial class ClothPage : Page
    {
        public ClothPage()
        {
            InitializeComponent();
            var db = UserData.Db;
            db.Clothes.Load();
            DbGrid.ItemsSource = db.Clothes.Local.ToBindingList();
            UserData.Grid = DbGrid;
        }
    }
}
