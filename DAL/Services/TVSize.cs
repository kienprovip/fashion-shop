using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVSize
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public void AddSize(string size_ten)
        {
            MySqlCommand command = new MySqlCommand("sp_AddSize", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@size_ten", size_ten);
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
        public List<size> GetSize()
        {
            List<size> sz = new List<size>();
            MySqlCommand cmd = new MySqlCommand("sp_GetSize", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    size szs = new size();
                    szs.size_name = reader.GetString(0);
                    sz.Add(szs);
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
            return sz;
        }
    }
}