using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVPay
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public List<pay> GetPay()
        {
            List<pay> p = new List<pay>();
            MySqlCommand cmd = new MySqlCommand("sp_GetPay", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pay ps = new pay();
                    ps.pay_id = reader.GetInt32(0);
                    ps.pay_name_vt = reader.GetString(1);
                    ps.pay_name_dd = reader.GetString(2);
                    p.Add(ps);
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
        public List<payment> GetPayment()
        {
            List<payment> p = new List<payment>();
            MySqlCommand cmd = new MySqlCommand("sp_GetPayment", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    payment ps = new payment();
                    ps.accounnt_username = reader.GetString(0);
                    ps.pay_id = reader.GetInt32(1);
                    ps.pay_number = reader.GetString(2);
                    ps.pay_name = reader.GetString(3);
                    ps.pay_password = reader.GetInt32(4);
                    p.Add(ps);
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
        public void AddPayment(string user, int pay_ma, string pay_so, int pay_matkhau, string pay_ten)
        {
            MySqlCommand command = new MySqlCommand("sp_AddPayment", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@pay_ma", pay_ma);
                command.Parameters.AddWithValue("@pay_so", pay_so);
                command.Parameters.AddWithValue("@pay_matkhau", pay_matkhau);
                command.Parameters.AddWithValue("@pay_ten", pay_ten);
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
        public List<getpay> GetPayForCustomer(string user)
        {
            List<getpay> p = new List<getpay>();
            MySqlCommand cmd = new MySqlCommand("sp_GetPayForCustomer", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user", user);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    getpay ps = new getpay();
                    ps.pay_id = reader.GetInt32(0);
                    ps.pay_name = reader.GetString(1);
                    ps.pay_name_vt = reader.GetString(2);
                    ps.pay_name_dd = reader.GetString(3);
                    p.Add(ps);
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
    }
}