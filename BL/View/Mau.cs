using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.View
{
    public class Mau
    {
        public void GetAll()
        {
            Console.WriteLine("ALL COLORS");
            TVColor m = new TVColor();
            List<color> lst = m.GetColor();
            if (lst.Count == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+-----------------+");
                Console.WriteLine("|  Color Name     |");
                Console.WriteLine("+-----------------+");
                foreach (color c in lst)
                {
                    Console.WriteLine("| {0, -16}|", c.color_name);
                    Console.WriteLine("+-----------------+");
                }
            }
        }
    }
}