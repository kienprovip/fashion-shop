using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVColor
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public void AddColor(string color_ten)
        {
            MySqlCommand command = new MySqlCommand("sp_AddColor", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@color_ten", color_ten);
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
        public List<color> GetColor()
        {
            List<color> cl = new List<color>();
            MySqlCommand cmd = new MySqlCommand("sp_GetColor", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    color cls = new color();
                    cls.color_name = reader.GetString(0);
                    cl.Add(cls);
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
            return cl;
        }
    }
}