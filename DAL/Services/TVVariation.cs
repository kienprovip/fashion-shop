using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVVariation
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public List<variation> GetAllVariation()
        {
            List<variation> vrat = new List<variation>();
            MySqlCommand cmd = new MySqlCommand("sp_GetVariation", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    variation pcz = new variation();
                    pcz.product_id = reader.GetInt32(0);
                    pcz.variation_id = reader.GetInt32(1);
                    pcz.color_name = reader.GetString(2);
                    pcz.size_name = reader.GetString(3);
                    pcz.product_quantity = reader.GetInt32(4);
                    pcz.variation_TrangThai = reader.GetString(5);
                    vrat.Add(pcz);
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
            return vrat;
        }
        public void AddVariation(int product_ma, string size_ten, string color_ten, int product_soluong, string variation_trangthai)
        {
            MySqlCommand command = new MySqlCommand("sp_AddVariation", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@product_ma", product_ma);
                command.Parameters.AddWithValue("@color_ten", color_ten);
                command.Parameters.AddWithValue("@size_ten", size_ten);
                command.Parameters.AddWithValue("@product_soluong", product_soluong);
                command.Parameters.AddWithValue("@variation_trangthai", variation_trangthai);
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
        public void UpdateVariation(int variation_ma, string color_ten, string size_ten, int product_slg, string variation_tgth)
        {
            MySqlCommand command = new MySqlCommand("sp_UpdateVariation", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@color_ten", color_ten);
                command.Parameters.AddWithValue("@size_ten", size_ten);
                command.Parameters.AddWithValue("@product_soluong", product_slg);
                command.Parameters.AddWithValue("@variation_status", variation_tgth);
                command.Parameters.AddWithValue("@variation_ma", variation_ma);
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
        public void DeleteVariation(int variation_ma)
        {
            MySqlCommand command = new MySqlCommand("sp_DeleteVariation", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@variation_ma", variation_ma);
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
        public void UpdateQuantity(int soluong, string trangthai, int ma)
        {
            MySqlCommand cmd = new MySqlCommand("sp_UpdateVariationQuantity", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@soluong", soluong);
                cmd.Parameters.AddWithValue("@trangthai", trangthai);
                cmd.Parameters.AddWithValue("@ma", ma);
                cmd.ExecuteNonQuery();
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
    }
}