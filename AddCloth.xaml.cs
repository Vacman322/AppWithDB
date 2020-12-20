using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace AppWithDB
{
    /// <summary>
    /// Interaction logic for AddCloth.xaml
    /// </summary>
    public partial class AddCloth : Window
    {
        public string FileName;
        public AddCloth()
        {
            InitializeComponent();
        }

        private void ChoiceFileButtonClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "JPEG images|*.jpg";
            openFileDialog1.Title = "Выбрать картинку";
            if ((bool)openFileDialog1.ShowDialog())
            {
                FileName = openFileDialog1.FileName;
                MessageBox.Show("Файл выбран");
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (!HelperClass.IsAllTextFieldsFill(this))
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }

            UserData.Db.Clothes.Add(new Cloth()
            {
                clothCode = new Random().Next().ToString(),
                clothName = ClothNameTextBox.Text,
                clothWidth = int.Parse(widthTextBox.Text),
                clothLength = int.Parse(lenghtTextBox.Text),
                clothPrice = int.Parse(priceTextBox.Text),
                clothPicture = FileName
            });

            try
            {
                UserData.Db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении в БД: " + ex);
            }
        }

        private void DigitOnly(object sender, TextCompositionEventArgs e)
        {
            HelperClass.DigitOnlyInput(e);
        }
    }
}
