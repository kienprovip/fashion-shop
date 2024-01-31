using DAL.DBContext;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class TVAccount
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public List<account> GetAccount()
        {
            List<account> getaccounts = new List<account>();
            MySqlCommand cmd = new MySqlCommand("sp_GetAccount", connection);
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    account a = new account();
                    a.account_username = reader.GetString(0);
                    a.account_password = reader.GetString(1);
                    a.name = reader.GetString(2);
                    a.phonenumber = reader.GetString(3);
                    a.email = reader.GetString(4);
                    a.account_TrangThai = reader.GetString(5);
                    getaccounts.Add(a);
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
            return getaccounts;
        }
        // check tài khoản khách hay là tài khoản của admin
        public bool accountCheck(string username, string password)
        {
            MySqlCommand command = new MySqlCommand("sp_CheckAccount", connection);
            bool accountcheck = false;
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string userName = reader["account_username"].ToString();
                    string passWord = reader["account_password"].ToString();
                    if (userName == "shopthoitrang" && passWord == "adminshopthoitrang")
                    {
                        accountcheck = true;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return accountcheck;
        }
        // thêm mới tài khoản
        public void AddUser(string username, string password, string name, string phone, string Email)
        {
            MySqlCommand command = new MySqlCommand("sp_AddAccount", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@tendangnhap", username);
                command.Parameters.AddWithValue("@matkhau", password);
                command.Parameters.AddWithValue("@ten", name);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@Email", Email);
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
        //sửa tài khoản
        public void UpdateAccountInfoName(string account_username, string value)
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("sp_UpdateAccountName", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@value_name", value);
                command.Parameters.AddWithValue("@username", account_username);
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
        public void UpdateAccountInfoPhone(string account_username, string value)
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("sp_UpdateAccountPhone", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@value_name", value);
                command.Parameters.AddWithValue("@username", account_username);
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
        public void UpdateAccountInfoEmail(string account_username, string value)
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("sp_UpdateAccountEmail", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@value_name", value);
                command.Parameters.AddWithValue("@username", account_username);
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
        public void UpdateAccountInfoPass(string account_username, string value)
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("sp_UpdateAccountPass", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@value_name", value);
                command.Parameters.AddWithValue("@username", account_username);
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
        //xóa tài khoản
        public void DeleteAccount(string username)
        {

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("sp_DeleteAccount", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", username);
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