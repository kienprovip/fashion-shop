using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVOrderPay
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public void AddOrderPay(int ma, int pay_ma, int address_ma)
        {
            MySqlCommand command = new MySqlCommand("sp_AddOrderPay", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ma", ma);
                command.Parameters.AddWithValue("@pay_ma", pay_ma);
                command.Parameters.AddWithValue("@address_ma", address_ma);
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