using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVProduct
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public List<product> GetProduct()
        {
            List<product> getproduct = new List<product>();
            MySqlCommand cmd = new MySqlCommand("sp_GetProduct", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product p = new product();
                    p.product_id = reader.GetInt32(0);
                    p.category_name = reader.GetString(1);
                    p.product_name = reader.GetString(2);
                    p.product_price = reader.GetFloat(3);
                    p.product_TrangThai = reader.GetString(4);
                    getproduct.Add(p);
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
            return getproduct;
        }

        // thêm sản phẩm
        public void AddProduct(int product_ma, string category_ten, string product_ten, float product_gia, string product_trangthai)
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("sp_AddProduct", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@product_ma", product_ma);
                command.Parameters.AddWithValue("@category_ten", category_ten);
                command.Parameters.AddWithValue("@product_gia", product_gia);
                command.Parameters.AddWithValue("@product_ten", product_ten);
                command.Parameters.AddWithValue("@product_trangthai", product_trangthai);
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
        // sửa thông tin sản phẩm
        public void UpdateProduct(int product_ma, string product_ten, string category_ten, float product_gia, string product_tgth)
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("sp_UpdateProduct", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@product_ten", product_ten);
                command.Parameters.AddWithValue("@category_ten", category_ten);
                command.Parameters.AddWithValue("@product_gia", product_gia);
                command.Parameters.AddWithValue("@product_ma", product_ma);
                command.Parameters.AddWithValue("@product_tgth", product_tgth);
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
        public void DeleteProduct(int product_ma)
        {
            MySqlCommand command = new MySqlCommand("sp_DeleteProduct", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@product_ma", product_ma);
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
    }
}