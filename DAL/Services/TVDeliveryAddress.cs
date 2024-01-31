using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace DAL.Services
{
    public class TVDeliveryAddress
    {
        private MySqlConnection connection = DbConnection.Instance.GetConnection();
        public void AddAddress(string account_username, string consignee_name, string consignee_phonenumber, string matinh, string mahuyen, string maxa, string consignee_address)
        {
            MySqlCommand command = new MySqlCommand("sp_AddAddress", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@account_username", account_username);
                command.Parameters.AddWithValue("@consignee_name", consignee_name);
                command.Parameters.AddWithValue("@consignee_phonenumber", consignee_phonenumber);
                command.Parameters.AddWithValue("@matinh", matinh);
                command.Parameters.AddWithValue("@mahuyen", mahuyen);
                command.Parameters.AddWithValue("@maxa", maxa);
                command.Parameters.AddWithValue("@consignee_address", consignee_address);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi " + ex);
            }
            finally
            {
                connection.Close();
            }
        }
        public List<tinhthanhpho> GetTinh(int pageSize, int startIndex)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetTinh", connection);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@pageSize", pageSize);
            List<tinhthanhpho> lsttinh = new List<tinhthanhpho>();
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tinhthanhpho ttp = new tinhthanhpho();
                    ttp.matp = reader.GetString(0);
                    ttp.name = reader.GetString(1);
                    lsttinh.Add(ttp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            finally
            {
                connection.Close();
            }
            return lsttinh;
        }
        public List<quanhuyen> GetHuyen(int pageSize, int startIndex, string matinh)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetHuyen", connection);
            cmd.Parameters.AddWithValue("@matinh", matinh);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@pageSize", pageSize);
            List<quanhuyen> lsthuyen = new List<quanhuyen>();
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    quanhuyen qh = new quanhuyen();
                    qh.matp = reader.GetString(0);
                    qh.maqh = reader.GetString(1);
                    qh.name = reader.GetString(2);
                    lsthuyen.Add(qh);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            finally
            {
                connection.Close();
            }
            return lsthuyen;
        }
        public List<xaphuongthitran> GetPhuong(int pageSize, int startIndex, string mahuyen)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetPhuong", connection);
            cmd.Parameters.AddWithValue("@mahuyen", mahuyen);
            cmd.Parameters.AddWithValue("@startIndex", startIndex);
            cmd.Parameters.AddWithValue("@pageSize", pageSize);
            List<xaphuongthitran> lstphuong = new List<xaphuongthitran>();
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    xaphuongthitran xptt = new xaphuongthitran();
                    xptt.maqh = reader.GetString(0);
                    xptt.xaid = reader.GetString(1);
                    xptt.name = reader.GetString(2);
                    lstphuong.Add(xptt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            finally
            {
                connection.Close();
            }
            return lstphuong;
        }
        public List<delivery_address> GetAllDeliveryAddress(string account_username)
        {
            MySqlCommand cmd = new MySqlCommand("sp_GetAddress", connection);
            List<delivery_address> dlveadr = new List<delivery_address>();
            try
            {
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("username", account_username);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    delivery_address d = new delivery_address();
                    d.delivery_address_id = reader.GetInt32(0);
                    d.consignee_name = reader.GetString(1);
                    d.consignee_phonenumber = reader.GetString(2);
                    d.fulladdress = reader.GetString(3);
                    d.delivery_address_TrangThai = reader.GetString(4);
                    dlveadr.Add(d);
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
            return dlveadr;
        }
        public void UpdateAddress(string consignee_ten, string consignee_sdt, string matinh, string mahuyen, string maxa, string consignee_dc, int ma, string tdn)
        {
            MySqlCommand command = new MySqlCommand("sp_UpdateAddress", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@consignee_ten", consignee_ten);
                command.Parameters.AddWithValue("@consignee_sdt", consignee_sdt);
                command.Parameters.AddWithValue("@matinh", matinh);
                command.Parameters.AddWithValue("@mahuyen", mahuyen);
                command.Parameters.AddWithValue("@maxa", maxa);
                command.Parameters.AddWithValue("@consignee_dc", consignee_dc);
                command.Parameters.AddWithValue("@ma", ma);
                command.Parameters.AddWithValue("@tdn", tdn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi " + ex);
            }
            finally
            {
                connection.Close();
            }
        }
        public void DeleteAddress(int ma, string tdn)
        {
            MySqlCommand command = new MySqlCommand("sp_DeleteAddress", connection);
            try
            {
                connection.Open();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ma", ma);
                command.Parameters.AddWithValue("@tdn", tdn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi " + ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}