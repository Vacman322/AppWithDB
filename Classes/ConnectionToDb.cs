using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithDB
{
    static class ConnectionToDb
    {
        public static string ConnectionString { get; } = "Data Source=desktop-v43mklr;Initial Catalog=Drapery;Integrated Security=True";

        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
