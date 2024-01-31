using System.Reflection.Metadata;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL.Services
{
    public class TVLogIn
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public bool Log(string username, string password)
        {
            MySqlCommand command = new MySqlCommand("sp_DangNhap", connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            bool login = false;
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                int result = Convert.ToInt32(command.ExecuteScalar());
                if (result > 0)
                {
                    login = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return login;
        }

    }
}