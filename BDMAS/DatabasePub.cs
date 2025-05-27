using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Blood_donation
{
    public static class DatabasePub
    {

        public static string Datasource = "localhost";
        public static string Database = "dbbghb";
        public static string Username = "root";
        public static string Password = "";

        public static string ConnectionString => $"DATASOURCE={Datasource};DATABASE={Database};USERNAME={Username};PASSWORD={Password};";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
