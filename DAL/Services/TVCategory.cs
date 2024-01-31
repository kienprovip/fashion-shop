using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVCategory
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        // thêm danh mục mới
        public void AddCategory(string category_ten)
        {
            MySqlCommand command = new MySqlCommand("sp_AddCategory", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@category_ten", category_ten);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
            finally
            {
                connection.Close();
            }
        }
        // hiển thị toàn bộ danh mục
        public List<category> GetAllCategory()
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetCategory", connection);
            List<category> categories = new List<category>();
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    category c = new category();
                    c.category_name = reader.GetString(0);
                    categories.Add(c);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
            finally
            {
                connection.Close();
            }
            return categories;
        }
    }
}