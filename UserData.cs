using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AppWithDB
{
    public static class UserData
    {
        public static string login;
        public static DraperyEntities Db;
        public static DataGrid Grid;
        public static TableName CurrentTableName;
    }
}
