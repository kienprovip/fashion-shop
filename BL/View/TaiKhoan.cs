using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.View
{
    public class TaiKhoan
    {
        public void GetAll()
        {
            TVAccount acc = new TVAccount();
            List<account> accountList = acc.GetAccount();
            var kh = accountList.Where(x => x.account_TrangThai == "on");
            if (kh.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+---------------+--------------+--------------------------------+");
                Console.WriteLine("| Name          | Phone Number | Email                          |");
                Console.WriteLine("+---------------+--------------+--------------------------------+");
                foreach (account ac in kh)
                {
                    Console.WriteLine("| {0, -14}| {1, -13}| {2, -31}|", ac.name, ac.phonenumber, ac.email);
                    Console.WriteLine("+---------------+--------------+--------------------------------+");
                }
            }
        }
        public void GetTK(string value)
        {
            TVAccount tv = new TVAccount();
            List<account> lst = tv.GetAccount();
            var result = lst.Where(x => x.account_username == value);
            Console.WriteLine("+---------------+--------------+--------------------------------+");
            Console.WriteLine("| Name          | Phone Number | Email                          |");
            Console.WriteLine("+---------------+--------------+--------------------------------+");
            foreach (account ac in result)
            {
                Console.WriteLine("| {0, -14}| {1, -13}| {2, -31}|", ac.name, ac.phonenumber, ac.email);
                Console.WriteLine("+---------------+--------------+--------------------------------+");
            }
        }
    }
}