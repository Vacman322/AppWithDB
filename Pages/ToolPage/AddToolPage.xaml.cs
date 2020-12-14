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
    /// Interaction logic for AddOrderToolPage.xaml
    /// </summary>
    public partial class AddToolPage : Page
    {
        public List<OrderedRecord> ordered;
        public AddToolPage()
        {
            InitializeComponent();
            if (UserData.orderedRecords == null)
            {
                UserData.orderedRecords = new List<OrderedRecord>();
                UserData.orderedRecords.Add(new OrderedRecord());
            }
            AddOrderDataGrid.ItemsSource = UserData.orderedRecords;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            UserData.orderedRecords.Add(new OrderedRecord());
            AddOrderDataGrid.Items.Refresh();
        }

        private void DelButtonClick(object sender, RoutedEventArgs e)
        {
            UserData.orderedRecords.Remove(AddOrderDataGrid.SelectedItem as OrderedRecord);
            AddOrderDataGrid.Items.Refresh();
        }

        private void AddSelectedButtonClick(object sender, RoutedEventArgs e)
        {
            if (UserData.Grid.SelectedItem != null && AddOrderDataGrid.Items.Count > 0)
            {
                switch (UserData.CurrentTableName)
                {
                    case TableName.product:
                        {
                            UserData.orderedRecords.Last().Product = UserData.Grid.SelectedItem as Product;
                        }
                        break;
                    case TableName.cloth:
                        {
                            UserData.orderedRecords.Last().Cloth = UserData.Grid.SelectedItem as Cloth;
                        }
                        break;
                    case TableName.furniture:
                        {
                            UserData.orderedRecords.Last().Furniture = UserData.Grid.SelectedItem as Furniture;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
