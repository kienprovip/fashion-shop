using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.View
{
    public class KichThuoc
    {
        public void GetAll()
        {
            Console.WriteLine("ALL SIZES");
            TVSize kt = new TVSize();
            List<size> lst = kt.GetSize();
            if (lst.Count == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+-----------------+");
                Console.WriteLine("|  Size Name      |");
                Console.WriteLine("+-----------------+");
                foreach (size s in lst)
                {
                    Console.WriteLine("| {0, -16}|", s.size_name);
                    Console.WriteLine("+-----------------+");
                }
            }
        }
    }
}