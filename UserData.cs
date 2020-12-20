using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AppWithDB
{
    public static class UserData 
    {
        public static User UserInf;
        public static DraperyEntities Db;
        public static DataGrid Grid;
        public static TableName CurrentTableName;
        public static OrderedRecords OrderedRecordsInf { get; set; }
    }

    public class OrderedRecords
    {
        public List<OrderedRecord> OrderedRecordsList { get; }
        public OrderedRecord Current { get; set; }

        public OrderedRecords()
        {
            OrderedRecordsList = new List<OrderedRecord>();
        }

        public void Add(OrderedRecord record)
        {
            OrderedRecordsList.Add(record);
            Current = record;
        }

        public void Remove(OrderedRecord record)
        {
            OrderedRecordsList.Remove(record);
            Current = OrderedRecordsList.LastOrDefault();
        }
    }

    public class OrderedRecord : INotifyPropertyChanged
    {
        Product product;
        Cloth cloth;
        Furniture furniture;

        public Product Product
        {
            get { return product; }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }
        public Cloth Cloth
        {
            get { return cloth; }
            set
            {
                cloth = value;
                OnPropertyChanged("Cloth");
            }
        }
        public Furniture Furniture
        {
            get { return furniture; }
            set
            {
                furniture = value;
                OnPropertyChanged("Furniture");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public bool IsFull()
        {
            return Product != null && Cloth != null && Furniture != null;
        }
    }
}
