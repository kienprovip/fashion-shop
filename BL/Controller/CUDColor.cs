using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.Controller
{
    public class CUDColor
    {
        public void Add()
        {
            int TCTGR = 0;
            string color_ten;
            try
            {
                TVColor addcolor = new TVColor();
                List<color> lst = addcolor.GetColor();
                do
                {
                    Console.WriteLine("ADD NEW COLOR");
                    Console.Write("Enter Color Name: ");
                    color_ten = Console.ReadLine();
                    var result = lst.Find(x => x.color_name == color_ten);
                    if (result == null)
                    {
                        addcolor.AddColor(color_ten);
                        Console.WriteLine("Successful");
                        TCTGR = 0;
                    }
                    else
                    {
                        Console.WriteLine("The Color Name You Entered Already exists");
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