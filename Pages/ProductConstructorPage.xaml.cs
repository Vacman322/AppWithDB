using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ProductConstructor.xaml
    /// </summary>
    public partial class ProductConstructorPage : Page
    {
        public ProductConstructorPage()
        {
            InitializeComponent();
        }

        private void AddClothButtonClick(object sender, RoutedEventArgs e)
        {
            var clothAddWindow = new AddCloth();
            clothAddWindow.Show();
        }

        private void viewButtonClick(object sender, RoutedEventArgs e)
        {
            Draw();
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            var fields = HelperClass.GetAllFieldsWithText(this);
            foreach (var field in fields)
            {
                var text = field.GetType().GetProperty("Text");
                text.SetValue(field, null);
            }

            canvas.Children.Clear();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (!HelperClass.IsAllTextFieldsFill(this))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }

            string prodName = productNameTextBox.Text;
            string clotnName = clothTypeComboBox.Text;
            var cloth = UserData.Db.Clothes.Where(c => c.clothName.Equals(clotnName)).FirstOrDefault();
            if (cloth == null)
            {
                MessageBox.Show("Не найдено указанной ткани");
                return;
            }

            int length = int.Parse(lenghtTextBox.Text);
            int width = int.Parse(lenghtTextBox.Text);

            string furType = furTypeComboBox.Text;
            var fur = UserData.Db.Furnitures.Where(f => f.furName.Equals(furType)).FirstOrDefault();
            if (fur == null)
            {
                MessageBox.Show("Не найдено указанной фурнитуры");
                return;
            }
            var prod = new Product()
            {
                productCode = new Random().Next().ToString(),
                productName = prodName,
                productWidth = width,
                productLength = length,
                Clothes = new List<Cloth>() { cloth }  
            };
            var prodFur = new ProductFurniture()
            {
                Product = prod,
                Furniture = fur,
                placement = "*",
                prodFurQty = 0
            };
            UserData.Db.Products.Add(prod);
            UserData.Db.ProductFurnitures.Add(prodFur);

            try
            {
                UserData.Db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении в БД: " + ex);
                return;
            }
            var result = MessageBox.Show("Сохранение прошло успешно! Очистить поля?", "Save", MessageBoxButton.YesNo,
           MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                ClearButtonClick(null, null);
        }

        private void OpenClothTypeCB(object sender, EventArgs e)
        {
            var listOfClothtype = UserData.Db.Clothes
                .Select(c => c.clothName)
                .Where(s => s.Contains(clothTypeComboBox.Text))
                .Distinct()
                .ToList();
            clothTypeComboBox.ItemsSource = listOfClothtype;
        }

        private void ClothTypeCBTextChanged(object sender, TextChangedEventArgs e)
        {
            if (clothTypeComboBox.IsDropDownOpen)
                OpenClothTypeCB(null, null);
            else
                clothTypeComboBox.IsDropDownOpen = true;
        }

        private void OpenFurTypeCB(object sender, EventArgs e)
        {
            var listOfFurType = UserData.Db.Furnitures
                .Select(f => f.furName)
                .Where(s => s.Contains(furTypeComboBox.Text))
                .Distinct()
                .ToList();
            furTypeComboBox.ItemsSource = listOfFurType;
        }

        private void FurTypeCBTextChanged(object sender, TextChangedEventArgs e)
        {
            if (furTypeComboBox.IsDropDownOpen)
                OpenFurTypeCB(null, null);
            else
                furTypeComboBox.IsDropDownOpen = true;
        }


        private void DigitOnly(object sender, TextCompositionEventArgs e)
        {
            HelperClass.DigitOnlyInput(e);
        }


        private void Draw()
        {

            if (!HelperClass.IsAllTextFieldsFill(this))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            int w = int.Parse(widthTextBox.Text);
            if (w > canvas.ActualWidth - 20)
            {
                w = Convert.ToInt32(canvas.ActualWidth - 20);
                MessageBox.Show("Ширина слишком большая");
            }
            int h = int.Parse(lenghtTextBox.Text);
            if (h > canvas.ActualHeight - 20)
            {
                h = Convert.ToInt32(canvas.ActualHeight - 20);
                MessageBox.Show("Высота слишком большая");
            }

            var rect = new Rectangle()
            {
                Fill = Brushes.White,
                Width = w,
                Height = h,
            };
            canvas.Children.Clear();
            canvas.Children.Add(rect);

            var cloth = UserData.Db.Clothes
                .Where(c => c.clothName == clothTypeComboBox.Text)
                .FirstOrDefault();
            string color = cloth.clothColor;
            string ris = cloth.clothPicture;

            if (ris != null && ris.Contains("jpg"))
            {
                ImageBrush img = new ImageBrush(new BitmapImage(new Uri(ris, UriKind.Relative)));
                rect.Fill = img;
            }
            else
            {
                switch (color?.ToLower())
                {
                    case "красный":
                        rect.Fill = Brushes.Red;
                        break;
                    case "зеленый":
                        rect.Fill = Brushes.Green;
                        break;
                    default:
                        rect.Fill = Brushes.Gray;
                        break;
                }
            }
        }

    }
}
