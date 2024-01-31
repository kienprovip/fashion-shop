using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.View
{
    public class ThanhToan
    {
        public void GetAll()
        {
            Console.WriteLine("ALL PAYS ");
            TVPay p = new TVPay();
            List<pay> lst = p.GetPay();
            if (lst.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+----+--------------+-----------------------------------------------------+");
                Console.WriteLine("| ID |  Pay Name    |                                                     |");
                Console.WriteLine("+----+--------------+-----------------------------------------------------+");
                foreach (pay py in lst)
                {
                    Console.WriteLine("| {0, -3}| {1,-13}| {2,-52}|", py.pay_id, py.pay_name_vt, py.pay_name_dd);
                    Console.WriteLine("+----+--------------+-----------------------------------------------------+");
                }
            }
        }
        public void GetPayment(string user)
        {
            TVPay tvpay = new TVPay();
            List<getpay> lstgetpay = tvpay.GetPayForCustomer(user);
            Console.WriteLine("+----+---------------+-----------+-----------------------------------------------------+");
            foreach (getpay p in lstgetpay)
            {
                Console.WriteLine("| {0, -3}| {1,-14}| {2,-10}| {3,-52}|", p.pay_id, p.pay_name, p.pay_name_vt, p.pay_name_dd);
                Console.WriteLine("+----+---------------+-----------+-----------------------------------------------------+");
            }
        }
    }
}