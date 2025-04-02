using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalonBellezaDesk_DEGM.DB_Connection
{
    public class Database
    {
        private static string connectionString = @"Data Source=C:\Users\dg678\OneDrive\Escritorio\PROYECTOS PROPIOS\BasesDatos\SalonBelleza.db;";
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }
        
    }
}
