using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVProductVariation
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public List<getproduct> GetAllHienthi()
        {
            List<getproduct> hienthis = new List<getproduct>();
            MySqlCommand cmd = new MySqlCommand("sp_GetAllProductVariation", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    getproduct ht = new getproduct();
                    ht.product_id = reader.GetInt32(0);
                    ht.category_name = reader.GetString(1);
                    ht.product_name = reader.GetString(2);
                    ht.color_name = reader.GetString(3);
                    ht.size_name = reader.GetString(4);
                    ht.product_price = reader.GetFloat(5);
                    ht.product_quantity = reader.GetInt32(6);
                    ht.product_TrangThai = reader.GetString(7);
                    ht.variation_TrangThai = reader.GetString(8);
                    hienthis.Add(ht);
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
            return hienthis;
        }
        public List<getproduct> GetAllProductVariation(int pageSize, int startIndex)
        {
            List<getproduct> getprd = new List<getproduct>();
            MySqlCommand cmd = new MySqlCommand("sp_GetProductVariationLimit", connection);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@pageSize", pageSize);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    getproduct ht = new getproduct();
                    ht.product_id = reader.GetInt32(0);
                    ht.category_name = reader.GetString(1);
                    ht.product_name = reader.GetString(2);
                    ht.color_name = reader.GetString(3);
                    ht.size_name = reader.GetString(4);
                    ht.product_price = reader.GetFloat(5);
                    ht.product_quantity = reader.GetInt32(6);
                    ht.product_TrangThai = reader.GetString(7);
                    ht.variation_TrangThai = reader.GetString(8);
                    getprd.Add(ht);
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
            return getprd;
        }
    }
}