using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class DbConnection
    {
        private static DbConnection instance;
        private MySqlConnection connection;

        private DbConnection()
        {
            // Khởi tạo kết nối cơ sở dữ liệu
            connection = new MySqlConnection("host=localhost;user=root;database=shopthoitrang;password=1234;port=3306");
        }

        public static DbConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbConnection();
                }
                return instance;
            }
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }

}
