using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.View
{
    public class DanhMuc
    {
        public void GetAll()
        {
            Console.WriteLine("ALL CATEOGRIES");
            TVCategory ctgr = new TVCategory();
            List<category> categoryList = ctgr.GetAllCategory();
            if (categoryList.Count == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+-----------------+");
                Console.WriteLine("|  Category Name  |");
                Console.WriteLine("+-----------------+");
                foreach (category ca in categoryList)
                {
                    Console.WriteLine("| {0, -16}|", ca.category_name);
                    Console.WriteLine("+-----------------+");
                }
            }
        }
    }
}