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
    /// Interaction logic for AdminToolPage.xaml
    /// </summary>
    public partial class AdminToolPage : Page
    {
        public AdminPage ap;
        public AdminToolPage(AdminPage page)
        {
            InitializeComponent();

            ap = page;
        }

        private void RefreshButtonClick(object sender, RoutedEventArgs e)
        {
            UserData.Db.SaveChanges();
        }
        
        private void DelButtonClick(object sender, RoutedEventArgs e)
        {
            DelFromDb();
        }

        void DelFromDb()
        {
            switch (UserData.CurrentTableName)
            {
                case TableName.product:
                    DelFromTable<Product>(UserData.Db.Products);
                    break;
                case TableName.cloth:
                    DelFromTable<Cloth>(UserData.Db.Clothes);
                    break;
                case TableName.furniture:
                    DelFromTable<Furniture>(UserData.Db.Furnitures);
                    break;
                case TableName.order:
                    DelFromTable<Ord>( UserData.Db.Ords);
                    break;
                default:
                    break;
            }
        }

        void DelFromTable<T>(DbSet<T> ts) where T : class
        {
            if (UserData.Grid != null && UserData.Grid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < UserData.Grid.SelectedItems.Count; i++)
                {
                    T product = (T) UserData.Grid.SelectedItems[i];
                    if (product != null) ts.Remove(product);
                }
            }
            try
            {
                UserData.Db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении в БД: " + ex);
            }
        }
    }
}
