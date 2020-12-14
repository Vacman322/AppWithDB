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
    /// Interaction logic for ConstructorPage.xaml
    /// </summary>
    public partial class ProdConstructorPage : Page
    {
        public ProdConstructorPage()
        {
            InitializeComponent();
        }

        private void CreateOrderClick(object sender, RoutedEventArgs e)
        {
            int tmp = 1;

            if (string.IsNullOrEmpty(PlacementTextBox.Text) || string.IsNullOrEmpty(FurCountTextBox.Text))
            {
                MessageBox.Show("Введите все поля");
                return;
            }

            string placement = PlacementTextBox.Text;
            if (!FurCountTextBox.Text.Select(c => char.IsDigit(c)).Aggregate((b1,b2) => b1 && b2))
            {
                MessageBox.Show("Кол-во должно быть указано в цифрах");
                return;
            }  
            
            int furCount = int.Parse(FurCountTextBox.Text);

            int? furWidth = null;
            if (int.TryParse(FurWidthTextBox.Text, out tmp))
                furWidth = tmp;

            int? furLength = null;
            if (int.TryParse(FurLengthTextBox.Text, out tmp))
                furLength = tmp;

            int? furRotation = null;
            if (int.TryParse(FurRotationTextBox.Text, out tmp))
                furLength = tmp;

            var record = UserData.orderedRecords.Last();

            var prodFur = new ProductFurniture()
            {
                Furniture = record.Furniture,
                Product = record.Product,
                prodFurWidth = furWidth,
                prodFurLength = furLength,
                prodFurRotation = furRotation,
                prodFurQty = furCount,
                placement = placement
            };
            //if (record.Cloth.Products.Contains(record.Product) || UserData.Db.ProductFurnitures.Contains(prodFur))
            //{
            //    MessageBox.Show("Такая запись уже существует");
            //    return;
            //}
            UserData.Db.ProductFurnitures.Add(prodFur);
            record.Cloth.Products.Add(record.Product);

            try
            {
                UserData.Db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении в бд: " + ex);
                return;
            }
            MessageBox.Show("Сохранено успешно!");
        }
    }
}
