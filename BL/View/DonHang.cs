using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.View
{
    public class DonHang
    {
        public void GetAllOder()
        {
            TVOrderPayAddress tvod = new TVOrderPayAddress();
            List<getorder> geto = tvod.GetAllOder();
            if (geto.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                Console.WriteLine("| ID |Product Name| Name     |Phonenumber| Consignee Address                                        |Color |Size|Price($)|Order Datetime        |Pay Name   |Quantity|Status     |");
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                foreach (getorder od in geto)
                {
                    Console.WriteLine("| {0,-3}| {1,-11}| {2,-9}| {3,-10}|{4,-58}|{5,-6}|{6,-4}| {7,-7}|{8,-22}|{9,-11}| {10,-7}| {11, -10}|", od.order_id, od.product_name, od.consignee_name, od.consignee_phonenumber, od.consignee_address, od.color_name, od.size_name, od.order_price, od.order_datetime, od.pay_name, od.order_quantity, od.order_TrangThai);
                    Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                }
            }
        }
        public void GetOrderForCustomer(string user)
        {
            TVOrderPayAddress tvod = new TVOrderPayAddress();
            List<getorder> geto = tvod.GetAllOder();
            var result = geto.Where(x => x.account_username == user);
            if (result.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                Console.WriteLine("| ID |Product Name| Name     |Phonenumber| Consignee Address                                        |Color |Size|Price($)|Order Datetime        |Pay Name   |Quantity|Status     |");
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                foreach (getorder od in result)
                {
                    Console.WriteLine("| {0,-3}| {1,-11}| {2,-9}| {3,-10}|{4,-58}|{5,-6}|{6,-4}| {7,-7}|{8,-22}|{9,-11}| {10,-7}| {11, -10}|", od.order_id, od.product_name, od.consignee_name, od.consignee_phonenumber, od.consignee_address, od.color_name, od.size_name, od.order_price, od.order_datetime, od.pay_name, od.order_quantity, od.order_TrangThai);
                    Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                }
            }
        }
    }
}