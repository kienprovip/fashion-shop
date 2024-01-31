using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.Controller
{
    public class CUDCategory
    {
        public void Add()
        {
            int TCTGR = 0;
            string category_ten;
            try
            {
                TVCategory addcategory = new TVCategory();
                List<category> lst = addcategory.GetAllCategory();
                do
                {
                    Console.WriteLine("ADD NEW CATEGORY");
                    Console.Write("Enter Category Name: ");
                    category_ten = Console.ReadLine();
                    string value = category_ten;
                    var result = lst.Find(x => x.category_name == category_ten);
                    if (result == null)
                    {
                        addcategory.AddCategory(category_ten);
                        Console.WriteLine("Successful");
                        TCTGR = 0;
                    }
                    else
                    {
                        Console.WriteLine("The Category Name You Entered Already exists");
                        do
                        {
                            Console.WriteLine("Do You Want To Try Again Or Exit?");
                            Console.WriteLine("1. Try Again");
                            Console.WriteLine("0. Exit");
                            Console.Write("--> ");
                            TCTGR = int.Parse(Console.ReadLine());
                            if (TCTGR != 0 && TCTGR != 1)
                            {
                                Console.WriteLine("Choice Again");
                            }
                        } while (TCTGR != 0 && TCTGR != 1);
                    }
                } while (TCTGR != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }
    }
}