using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVOder
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public int AddOder(string username, int product_ma, int variation_ma, int order_soluong, float order_gia)
        {
            int lastInsertId = 1;
            MySqlCommand command = new MySqlCommand("sp_AddOrder", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@product_ma", product_ma);
                command.Parameters.AddWithValue("@variation_ma", variation_ma);
                command.Parameters.AddWithValue("@order_soluong", order_soluong);
                command.Parameters.AddWithValue("@order_gia", order_gia);
                lastInsertId = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
            finally
            {
                connection.Close();
            }
            return lastInsertId;
        }
        public List<getorder> ProductSold()
        {
            List<getorder> p = new List<getorder>();
            MySqlCommand cmd = new MySqlCommand("sp_ProductSold", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    getorder o = new getorder();
                    o.product_name = reader.GetString(0);
                    o.order_quantity = reader.GetInt32(1);
                    o.order_price = reader.GetFloat(2);
                    p.Add(o);
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
            return p;
        }
        public void AcceptOrder(string trangthai, int madonhang)
        {
            MySqlCommand cmd = new MySqlCommand("sp_AcceptOrder", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@trangthai", trangthai);
                cmd.Parameters.AddWithValue("@madonhang", madonhang);
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