﻿using AppWithDB.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Input;

namespace AppWithDB
{
    public static class HelperClass
    {
        /// <summary>
        /// Переводит Page на Frame, если Page равна Null то присваивает ей значение и указывает данные в UserData
        /// </summary>
        public static T ShowPage<T>(Frame frame, T page, TableName tableName, bool isReadOnlyGrid = true) where T : new()
        {
            if (page == null) page = new T();
            frame.Navigate(page);
            UserData.CurrentTableName = tableName;

            if (tableName == TableName.construct) return page;

            var t = page.GetType();
            var i = t.GetField("DbGrid", BindingFlags.NonPublic | BindingFlags.Instance);
            UserData.Grid = i.GetValue(page) as DataGrid;
            UserData.Grid.IsReadOnly = isReadOnlyGrid;

            //switch (tableName)
            //{
            //    case TableName.product:
            //        {
            //            var p = page as ProductPadge;
            //            UserData.Grid = p.DbGrid;
            //            UserData.Grid.IsReadOnly = isReadOnlyGrid;
            //        }
            //        break;
            //    case TableName.cloth:
            //        {
            //            var p = page as ClothPage;
            //            UserData.Grid = p.DbGrid;
            //            UserData.Grid.IsReadOnly = isReadOnlyGrid;
            //        }
            //        break;
            //    case TableName.furniture:
            //        {
            //            var p = page as FurniturePage;
            //            UserData.Grid = p.DbGrid;
            //            UserData.Grid.IsReadOnly = isReadOnlyGrid;
            //        }
            //        break;
            //    case TableName.order:
            //        {
            //            var p = page as OrderPage;
            //            UserData.Grid = p.DbGrid;
            //            UserData.Grid.IsReadOnly = isReadOnlyGrid;
            //        }
            //        break;
            //    default:
            //        break;
            //}
            return page;
        }

        public static List<object> GetAllFieldsWithText(object obj)
        {
            return obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.FieldType == typeof(TextBox) || f.FieldType == typeof(ComboBox))
                .Select(f => f.GetValue(obj))
                .ToList();
        }

        public static bool IsAllTextFieldsFill(object obj)
        {
            var fields = HelperClass.GetAllFieldsWithText(obj);
            return fields
                .Select(f => !string.IsNullOrEmpty(f.GetType().GetProperty("Text").GetValue(f) as string))
                .Aggregate((b1, b2) => b1 && b2);
        }

        public static void DigitOnlyInput(TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Удаляет выбраные данные в текущей таблице которая указана в UserData
        /// </summary>
        public static void DelFromDb()
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
                    DelFromTable<Ord>(UserData.Db.Ords);
                    break;
                default:
                    break;
            }
            try
            {
                UserData.Db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении в БД: " + ex);

                UserData.Db.Dispose();
                UserData.Db = new DraperyEntities();

                switch (UserData.CurrentTableName)
                {
                    case TableName.product:
                        {
                            UserData.Db.Products.Load();
                            UserData.Grid.ItemsSource = UserData.Db.Products.Local.ToBindingList();
                        }
                        break;
                    case TableName.cloth:
                        {
                            UserData.Db.Clothes.Load();
                            UserData.Grid.ItemsSource = UserData.Db.Clothes.Local.ToBindingList();
                        }
                        break;
                    case TableName.furniture:
                        {
                            UserData.Db.Furnitures.Load();
                            UserData.Grid.ItemsSource = UserData.Db.Furnitures.Local.ToBindingList();
                        }
                        break;
                    case TableName.order:
                        {
                            UserData.Db.Ords.Load();
                            UserData.Grid.ItemsSource = UserData.Db.Ords.Local.ToBindingList();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public static void DelFromTable<T>(DbSet<T> ts) where T : class
        {
            T record = null;
            if (UserData.Grid != null && UserData.Grid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < UserData.Grid.SelectedItems.Count; i++)
                {
                    record = (T)UserData.Grid.SelectedItems[i];
                    if (record != null) ts.Remove(record);
                }
            }
        }
    }
}
