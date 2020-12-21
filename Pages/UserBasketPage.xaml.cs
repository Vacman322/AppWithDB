using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class BasketTableItem
    {
        public Product Prod { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }

        public BasketTableItem (Product product, int qty, int price)
        {
            Prod = product;
            Qty = qty;
            Price = price;
        }
    }
    /// <summary>
    /// Interaction logic for UserOrderPage.xaml
    /// </summary>
    public partial class UserBasketPage : Page
    {
        public ObservableCollection<BasketTableItem> BasketList;
        public UserBasketPage()
        {
            InitializeComponent();
            BasketList = new ObservableCollection<BasketTableItem>();
            BasketDataGrid.ItemsSource = BasketList;
        }

        private void ProdNameComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ProdNameComboBox.IsDropDownOpen)
                ProdNameComboBox_DropDownOpened(null, null);
            else
                ProdNameComboBox.IsDropDownOpen = true;

            CalcPrice();
        }

        private void ProdNameComboBox_DropDownOpened(object sender, EventArgs e)
        {
            var prodNameList = UserData.Db.Products
                .Select(p => p.productName)
                .Where(s => s.Contains(ProdNameComboBox.Text))
                .Distinct()
                .ToList();
            ProdNameComboBox.ItemsSource = prodNameList;
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            var fields = HelperClass.GetAllFieldsWithText(this);
            foreach (var field in fields)
            {
                field.GetType().GetProperty("Text").SetValue(field, null);
            }
            ProdNameComboBox.IsDropDownOpen = false;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (!HelperClass.IsAllTextFieldsFill(this))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }

            var prod = UserData.Db.Products
                .Where(p => p.productName.Equals(ProdNameComboBox.Text))
                .FirstOrDefault();
            if (prod == null)
            {
                MessageBox.Show("Указанного продукта не найдено");
                return;
            }

            int qty = int.Parse(QtyComboBox.Text);
            int price = int.Parse(PriceLabel.Content as string);


            BasketList.Add(new BasketTableItem(prod,qty,price));
            ClearButtonClick(null, null);
        }
        private void DigitOnly(object sender, TextCompositionEventArgs e)
        {
            HelperClass.DigitOnlyInput(e);
        }

        private void BasketDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BasketDataGrid.SelectedItem == null) return;

            var selected = BasketDataGrid.SelectedItem as BasketTableItem;
            ProdNameComboBox.Text = selected.Prod.productName;
            QtyComboBox.Text = selected.Qty.ToString();
        }

        private void DelButtonClick(object sender, RoutedEventArgs e)
        {
            if (BasketDataGrid.SelectedItem == null) return;

            var selected = BasketDataGrid.SelectedItem as BasketTableItem;
            BasketList.Remove(selected);
            ClearButtonClick(null, null);
        }

        private void QtyComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalcPrice();
        }

        public void CalcPrice()
        {
            if (!HelperClass.IsAllTextFieldsFill(this))
            {
                PriceLabel.Content = "";
                return;
            }
            var prod = UserData.Db.Products
                .Where(p => p.productName.Equals(ProdNameComboBox.Text))
                .FirstOrDefault();
            if (prod == null)
            {
                PriceLabel.Content = "";
                return;
            }
            int qty = int.Parse(QtyComboBox.Text);
            decimal endPrice = 1000;

            if (prod.Clothes.Count > 0)
            {
                endPrice += prod.Clothes.First().clothPrice;
            }

            if (prod.ProductFurnitures.Count > 0)
            {
                endPrice += prod.ProductFurnitures.First().Furniture.furPrice;
            }
            endPrice *= qty;

            PriceLabel.Content = endPrice.ToString();
        }

        private void BookButtonClick(object sender, RoutedEventArgs e)
        {
            if(BasketDataGrid.Items.Count == 0)
            {
                MessageBox.Show("Добавьте продукт");
                return;
            }

            foreach (BasketTableItem item in BasketDataGrid.Items)
            {
                var ord = new Ord()
                {
                    ordDate = DateTime.Now,
                    stageOfExecution = "Забронирован",
                    customerId = UserData.UserInf.userId,
                    ordPrice = item.Price
                };

                var ordered = new OrderedProduct()
                {
                    Product = item.Prod,
                    Ord = ord,
                    ProductQty = item.Qty
                };

                UserData.Db.Ords.Add(ord);
                UserData.Db.OrderedProducts.Add(ordered);

                try
                {
                    UserData.Db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении " +
                        item.Prod.productName + ":" + ex);
                    continue;
                }
            }
            BasketList.Clear();
        }
    }
}
