using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.Controller
{
    public class CUDSize
    {
        public void Add()
        {
            int TCTGR = 0;
            string size_ten;
            try
            {
                TVSize addsize = new TVSize();
                List<size> lst = addsize.GetSize();
                do
                {
                    Console.WriteLine("ADD NEW SIZE");
                    Console.Write("Enter Size Name: ");
                    size_ten = Console.ReadLine();
                    var result = lst.Find(x => x.size_name == size_ten);
                    if (result == null)
                    {
                        addsize.AddSize(size_ten);
                        Console.WriteLine("Successful");
                        TCTGR = 0;
                    }
                    else
                    {
                        Console.WriteLine("The Size Name You Entered Already exists");
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