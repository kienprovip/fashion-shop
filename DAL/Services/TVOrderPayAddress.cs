using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVOrderPayAddress
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public List<getorder> GetAllOder()
        {
            List<getorder> god = new List<getorder>();
            MySqlCommand cmd = new MySqlCommand("sp_GetOrderPayAddress", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    getorder od = new getorder();
                    od.account_username = reader.GetString(0);
                    od.order_id = reader.GetInt32(1);
                    od.product_name = reader.GetString(2);
                    od.consignee_name = reader.GetString(3);
                    od.consignee_phonenumber = reader.GetString(4);
                    od.consignee_address = reader.GetString(5);
                    od.color_name = reader.GetString(6);
                    od.size_name = reader.GetString(7);
                    od.order_quantity = reader.GetInt32(8);
                    od.order_price = reader.GetFloat(9);
                    od.order_datetime = reader.GetDateTime(10);
                    od.pay_name = reader.GetString(11);
                    od.order_TrangThai = reader.GetString(12);
                    od.variation_id = reader.GetInt32(13);
                    god.Add(od);
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
            return god;
        }
        public void DeleteOrderPayAddress(int ma)
        {
            MySqlCommand cmd = new MySqlCommand("sp_DeleteOrderPayAddress", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@macanxoa", ma);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}