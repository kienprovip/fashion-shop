using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;
using MySql.Data.MySqlClient;

namespace BL.Controller
{
    public class CUDVariation
    {

        public void UpdateVariation()
        {
            try
            {
                int thulaibienthe;
                int bienthe;
                int product_ma = 0;
                int pageNumber = 1;
                int pageSize = 10;
                int startIndex = (pageNumber - 1) * pageSize;
                int endIndex = startIndex + pageSize - 1;
                bool exit = false;
                string color_ten = "";
                string size_ten = "";
                TVSize tvsize = new TVSize();
                List<size> lstsize = tvsize.GetSize();
                TVColor tvcolor = new TVColor();
                List<color> lstcolor = tvcolor.GetColor();
                TVCategory tvcategory = new TVCategory();
                List<category> lstcategory = tvcategory.GetAllCategory();
                TVVariation addvariation = new TVVariation();
                TVProduct tvproduct = new TVProduct();
                TVProductVariation tvprd = new TVProductVariation();
                List<getproduct> getsps = tvprd.GetAllProductVariation(pageSize, startIndex);
                List<variation> vr = addvariation.GetAllVariation();
                var result = getsps;
                List<getproduct> d = tvprd.GetAllHienthi();
                var dem = d;
                int count = dem.Count();
                int totalProducts = count;
                int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
                if (result.Count() == 0)
                {
                    Console.WriteLine("List Is Empty");
                }
                else
                {
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price    | Quantity |");
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    foreach (getproduct ht in result)
                    {
                        Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                        Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    }
                    Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                    while (!exit)
                    {
                        Console.WriteLine("Enter 'N' To Next Page");
                        Console.WriteLine("Enter 'P' To Back Page");
                        Console.WriteLine("Enter 'Q' To Exit");
                        Console.WriteLine("Enter Number Is Product ID to Update Variation");
                        Console.Write("--> ");
                        string input = Console.ReadLine();
                        if (input.ToLower() == "n")
                        {
                            totalProducts = count;
                            totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
                            if (pageNumber >= totalPages)
                            {
                                Console.WriteLine("   +-----------------+");
                                Console.WriteLine("-->|This Is Last Page|<--");
                                Console.WriteLine("   +-----------------+");
                                pageNumber = totalPages; // Đặt lại số trang hiện tại thành trang cuối cùng
                                endIndex = totalProducts = -1;
                            }
                            else
                            {
                                pageNumber++;
                                startIndex = (pageNumber - 1) * pageSize;
                                endIndex = startIndex + pageSize - 1;
                                List<getproduct> getsp = tvprd.GetAllProductVariation(pageSize, startIndex);
                                var result1 = getsp;
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price    | Quantity |");
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                foreach (getproduct ht in result1)
                                {
                                    Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                }
                                Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                            }
                        }
                        else if (input.ToLower() == "p")
                        {
                            if (pageNumber > 1)
                            {
                                pageNumber--;
                                startIndex = (pageNumber - 1) * pageSize;
                                endIndex = startIndex + pageSize - 1;
                                List<getproduct> getspt = tvprd.GetAllProductVariation(pageSize, startIndex);
                                var result2 = getspt;
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price    | Quantity |");
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                foreach (getproduct ht in result2)
                                {
                                    Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                }
                                Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                            }
                            else
                            {
                                Console.WriteLine("   +------------------+");
                                Console.WriteLine("-->|This Is First Page|<--");
                                Console.WriteLine("   +------------------+");
                            }
                        }
                        else if (input.ToLower() == "q")
                        {
                            exit = true;
                            Console.WriteLine("Exited");
                        }
                        else
                        {
                            if (int.TryParse(input, out product_ma))
                            {
                                int a = pageNumber;
                                int b = (a - 1) * pageSize;
                                int c = b + pageSize - 1;
                                List<getproduct> q = tvprd.GetAllProductVariation(pageSize, b);
                                var getma = q.Where(x => x.product_id == product_ma);
                                var bt = vr.Where(x => x.product_id == product_ma);
                                if (getma.Count() == 0)
                                {
                                    Console.WriteLine("Not Found");
                                }
                                else
                                {
                                    var bth = vr.Where(x => x.product_id == product_ma);
                                    Console.WriteLine("+----+------------+--------+----------+--------+");
                                    Console.WriteLine("| ID | Color      | Size   | Quantity | Status |");
                                    Console.WriteLine("+----+------------+--------+----------+--------+");
                                    foreach (variation ht in bth)
                                    {
                                        Console.WriteLine("| {0,-3}| {1,-11}| {2,-7}| {3,-9}| {4,-7}|", ht.variation_id, ht.color_name, ht.size_name, ht.product_quantity, ht.variation_TrangThai);
                                        Console.WriteLine("+----+------------+--------+----------+--------+");
                                    }
                                    do
                                    {
                                        Console.Write("Enter The Variation ID To Update: ");
                                        bienthe = int.Parse(Console.ReadLine());
                                        var kiemtrabienthe = vr.Where(x => x.variation_id == bienthe);
                                        thulaibienthe = kiemtrabienthe.Count();
                                        if (thulaibienthe == 0)
                                        {
                                            Console.WriteLine("Not Found, Please Try Again");
                                        }
                                    } while (thulaibienthe == 0);
                                    Console.Write("List Of Available Colors: | ");
                                    foreach (color cl in lstcolor)
                                    {
                                        Console.Write(cl.color_name + " | ");
                                    }
                                    int chonmau = 0;
                                    while (chonmau == 0)
                                    {
                                        Console.Write("\nEnter New Color: ");
                                        color_ten = Console.ReadLine();
                                        chonmau = lstcolor.Where(x => x.color_name == color_ten).Count();
                                        if (chonmau == 0)
                                        {
                                            Console.WriteLine("Try Again");
                                        }
                                    }
                                    int tiepsize = 0;
                                    Console.Write("List Of Available Sizes: | ");
                                    foreach (size sz in lstsize)
                                    {
                                        Console.Write(sz.size_name + " | ");
                                    }
                                    while (tiepsize == 0)
                                    {
                                        Console.Write("\nEnter New Size: ");
                                        size_ten = Console.ReadLine();
                                        tiepsize = lstsize.Where(x => x.size_name == size_ten).Count();
                                        if (tiepsize == 0)
                                        {
                                            Console.WriteLine("Try Again");
                                        }
                                    }
                                    int variation_soluong;
                                    do
                                    {
                                        Console.Write("Enter New Quantity Of This Variation: ");
                                        variation_soluong = int.Parse(Console.ReadLine());
                                        if (variation_soluong < 0)
                                        {
                                            Console.WriteLine("Try Again");
                                        }
                                    } while (variation_soluong < 0);
                                    string onof = "";
                                    do
                                    {
                                        Console.Write("Enter New Status Of This Variation(on/off): ");
                                        onof = Console.ReadLine();
                                        if (onof.ToLower() != "on" && onof.ToLower() != "off")
                                        {
                                            Console.WriteLine("Try Again");
                                        }
                                    } while (onof.ToLower() != "on" && onof.ToLower() != "off");
                                    string yn = "";
                                    do
                                    {
                                        Console.Write("Are You Sure?(Y/N): ");
                                        yn = Console.ReadLine();
                                        if (yn.ToLower() != "n" && yn.ToLower() != "y")
                                        {
                                            Console.WriteLine("Try Again");
                                        }
                                        else if (yn.ToLower() == "y")
                                        {
                                            addvariation.UpdateVariation(bienthe, color_ten, size_ten, variation_soluong, onof);
                                            Console.WriteLine("Successfully Updated");
                                            exit = true;
                                        }
                                        else if (yn.ToLower() == "n")
                                        {
                                            exit = true;
                                        }
                                    } while (yn.ToLower() != "n" && yn.ToLower() != "y");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
        }
        public void AddVariation()
        {
            try
            {
                int bienthe;
                string color_ten = "";
                int Ngia = 1;
                string size_ten = "";
                int product_ma = 0;
                int pageNumber = 1;
                int pageSize = 10;
                int startIndex = (pageNumber - 1) * pageSize;
                int endIndex = startIndex + pageSize - 1;
                bool exit = false;
                TVColor addcolor = new TVColor();
                TVSize addsize = new TVSize();
                TVVariation addvariation = new TVVariation();
                TVProductVariation tvprd = new TVProductVariation();
                List<getproduct> getsps = tvprd.GetAllProductVariation(pageSize, startIndex);
                List<variation> vr = addvariation.GetAllVariation();
                var result = getsps.Where(x => x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                List<getproduct> d = tvprd.GetAllHienthi();
                var dem = d.Where(x => x.product_TrangThai == "on");
                int count = dem.Count();
                int totalProducts = count;
                int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
                if (result.Count() == 0)
                {
                    Console.WriteLine("List Is Empty");
                }
                else
                {
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price    | Quantity |");
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    foreach (getproduct ht in result)
                    {
                        Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                        Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    }
                    Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                    while (!exit)
                    {
                        Console.WriteLine("Enter 'N' To Next Page");
                        Console.WriteLine("Enter 'P' To Back Page");
                        Console.WriteLine("Enter 'Q' To Exit");
                        Console.WriteLine("Enter Number Is Product ID to Add Variation");
                        Console.Write("--> ");
                        string input = Console.ReadLine();
                        if (input.ToLower() == "n")
                        {
                            totalProducts = count;
                            totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
                            if (pageNumber >= totalPages)
                            {
                                Console.WriteLine("   +-----------------+");
                                Console.WriteLine("-->|This Is Last Page|<--");
                                Console.WriteLine("   +-----------------+");
                                pageNumber = totalPages; // Đặt lại số trang hiện tại thành trang cuối cùng
                                endIndex = totalProducts = -1;
                            }
                            else
                            {
                                pageNumber++;
                                startIndex = (pageNumber - 1) * pageSize;
                                endIndex = startIndex + pageSize - 1;
                                List<getproduct> getsp = tvprd.GetAllProductVariation(pageSize, startIndex);
                                var result1 = getsp.Where(x => x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price    | Quantity |");
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                foreach (getproduct ht in result1)
                                {
                                    Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                }
                                Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                            }
                        }
                        else if (input.ToLower() == "p")
                        {
                            if (pageNumber > 1)
                            {
                                pageNumber--;
                                startIndex = (pageNumber - 1) * pageSize;
                                endIndex = startIndex + pageSize - 1;
                                List<getproduct> getspt = tvprd.GetAllProductVariation(pageSize, startIndex);
                                var result2 = getspt.Where(x => x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price    | Quantity |");
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                foreach (getproduct ht in result2)
                                {
                                    Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                }
                                Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                            }
                            else
                            {
                                Console.WriteLine("   +------------------+");
                                Console.WriteLine("-->|This Is First Page|<--");
                                Console.WriteLine("   +------------------+");
                            }
                        }
                        else if (input.ToLower() == "q")
                        {
                            exit = true;
                            Console.WriteLine("Exited");
                        }
                        else
                        {
                            if (int.TryParse(input, out product_ma))
                            {
                                int a = pageNumber;
                                int b = (a - 1) * pageSize;
                                int c = b + pageSize - 1;
                                List<getproduct> q = tvprd.GetAllProductVariation(pageSize, b);
                                var getma = q.Where(x => x.product_id == product_ma && x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                                var bt = vr.Where(x => x.product_id == product_ma && x.variation_TrangThai == "on");
                                if (getma.Count() == 0)
                                {
                                    Console.WriteLine("Not Found");
                                }
                                else
                                {
                                    do
                                    {
                                        Console.Write("How Many Variations Do You Want To Add?: ");
                                        bienthe = int.Parse(Console.ReadLine());
                                        if (bienthe <= 0)
                                        {
                                            Console.WriteLine("Variation > 0, Please Try Again");
                                        }
                                    } while (bienthe <= 0);
                                    for (int i = 1; i <= bienthe; i++)
                                    {
                                        int TCLR = 1;
                                        int TSZE = 1;
                                        int Dcl = 1;
                                        int Dsz = 1;
                                        Console.WriteLine("Variation {0}", i);
                                        while (Dcl != 0)
                                        {
                                            Console.Write("Enter Color: ");
                                            color_ten = Console.ReadLine();
                                            List<color> lstcolor = addcolor.GetColor();
                                            var checkcolor = lstcolor.Find(x => x.color_name == color_ten);
                                            if (checkcolor == null)
                                            {
                                                Console.WriteLine("Color You Entered Does Not Exist");
                                                do
                                                {
                                                    Console.WriteLine("Do You Want To Try Againt, Create New Color Or Exit");
                                                    Console.WriteLine("0. Exit");
                                                    Console.WriteLine("1. Creat New Color");
                                                    Console.WriteLine("2. Try Again");
                                                    Console.Write("--> ");
                                                    Dcl = int.Parse(Console.ReadLine());
                                                    if (Dcl != 0 && Dcl != 1 && Dcl != 2)
                                                    {
                                                        Console.WriteLine("Choice Again");
                                                    }
                                                } while (Dcl != 1 && Dcl != 0 && Dcl != 2);
                                                if (Dcl == 0)
                                                {
                                                    Ngia = 0;
                                                    Dsz = 0;
                                                    i = bienthe + 1;
                                                }
                                                else if (Dcl == 1)
                                                {
                                                    do
                                                    {
                                                        Console.Write("Enter Color You Want To Create: ");
                                                        color_ten = Console.ReadLine();
                                                        List<color> lsttcolor = addcolor.GetColor();
                                                        var checktcolor = lsttcolor.Find(x => x.color_name == color_ten);
                                                        if (checktcolor == null)
                                                        {
                                                            addcolor.AddColor(color_ten);
                                                            Console.WriteLine("Successful");
                                                            TCLR = 0;
                                                            Dcl = 0;
                                                            Ngia = 1;
                                                            Dsz = 1;
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
                                                                TCLR = int.Parse(Console.ReadLine());
                                                                if (TCLR != 0 && TCLR != 1)
                                                                {
                                                                    Console.WriteLine("Choice Again");
                                                                }
                                                            } while (TCLR != 1 && TCLR != 0);
                                                        }
                                                    } while (TCLR != 0);
                                                }
                                            }
                                            else
                                            {
                                                Dcl = 0;
                                                Dsz = 1;
                                                Ngia = 1;
                                            }
                                        }
                                        while (Dsz != 0)
                                        {
                                            Console.Write("Enter Size: ");
                                            size_ten = Console.ReadLine();
                                            List<size> lstsize = addsize.GetSize();
                                            var checksize = lstsize.Find(x => x.size_name == size_ten);
                                            if (checksize == null)
                                            {
                                                Console.WriteLine("Size You Entered Does Not Exist");
                                                do
                                                {
                                                    Console.WriteLine("Do You Want To Try Againt, Create New Size Or Exit");
                                                    Console.WriteLine("0. Exit");
                                                    Console.WriteLine("1. Creat New Size");
                                                    Console.WriteLine("2. Try Again");
                                                    Console.Write("--> ");
                                                    Dsz = int.Parse(Console.ReadLine());
                                                    if (Dsz != 0 && Dsz != 1 && Dsz != 2)
                                                    {
                                                        Console.WriteLine("Choice Again");
                                                    }
                                                } while (Dsz != 1 && Dsz != 0 && Dsz != 2);
                                                if (Dsz == 0)
                                                {
                                                    Ngia = 0;
                                                    i = bienthe + 1;
                                                }
                                                if (Dsz == 1)
                                                {
                                                    do
                                                    {
                                                        Console.Write("Enter Size You Want to Create: ");
                                                        size_ten = Console.ReadLine();
                                                        List<size> lsttsize = addsize.GetSize();
                                                        var checktsize = lsttsize.Find(x => x.size_name == size_ten);
                                                        if (checktsize == null)
                                                        {
                                                            addsize.AddSize(size_ten);
                                                            Console.WriteLine("Successful");
                                                            TSZE = 0;
                                                            Ngia = 1;
                                                            Dsz = 0;
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
                                                                TSZE = int.Parse(Console.ReadLine());
                                                                if (TSZE != 0 && TSZE != 1)
                                                                {
                                                                    Console.WriteLine("Choice Again");
                                                                }
                                                            } while (TSZE != 1 && TSZE != 0);
                                                        }
                                                    } while (TSZE != 0);
                                                }
                                            }
                                            else
                                            {
                                                Dsz = 0;
                                                Ngia = 1;
                                            }
                                        }
                                        if (Ngia == 1)
                                        {
                                            int product_soluong;
                                            do
                                            {
                                                Console.Write("Enter Quantity Of This Variation: ");
                                                product_soluong = int.Parse(Console.ReadLine());
                                                if (product_soluong < 0)
                                                {
                                                    Console.WriteLine("Quantity must greater or equal 0");
                                                }
                                            } while (product_soluong < 0);
                                            string product_color_size_trangthai = "on";
                                            addvariation.AddVariation(product_ma, size_ten, color_ten, product_soluong, product_color_size_trangthai);
                                            Console.WriteLine("Successfully Added New Variable");
                                            exit = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
            }
        }
        public void DeleteVariation()
        {
            int product_ma = 0;
            int pageNumber = 1;
            int pageSize = 10;
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = startIndex + pageSize - 1;
            bool exit = false;
            int mabienthe = 0;
            TVVariation tvvariation = new TVVariation();
            List<variation> lstvariation = tvvariation.GetAllVariation();
            TVProductVariation tvprd = new TVProductVariation();
            List<getproduct> getsps = tvprd.GetAllProductVariation(pageSize, startIndex);
            var result = getsps.Where(x => x.product_TrangThai == "on" && x.variation_TrangThai == "on");
            List<getproduct> d = tvprd.GetAllHienthi();
            var dem = d.Where(x => x.product_TrangThai == "on");
            int count = dem.Count();
            int totalProducts = count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            if (result.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price    | Quantity |");
                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                foreach (getproduct ht in result)
                {
                    Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                }
                Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                while (!exit)
                {
                    Console.WriteLine("Enter 'N' To Next Page");
                    Console.WriteLine("Enter 'P' To Back Page");
                    Console.WriteLine("Enter 'Q' To Exit");
                    Console.WriteLine("Enter Number Is Product ID to Delete");
                    Console.Write("--> ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "n")
                    {
                        totalProducts = count;
                        totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
                        if (pageNumber >= totalPages)
                        {
                            Console.WriteLine("   +-----------------+");
                            Console.WriteLine("-->|This Is Last Page|<--");
                            Console.WriteLine("   +-----------------+");
                            pageNumber = totalPages; // Đặt lại số trang hiện tại thành trang cuối cùng
                            endIndex = totalProducts = -1;
                        }
                        else
                        {
                            pageNumber++;
                            startIndex = (pageNumber - 1) * pageSize;
                            endIndex = startIndex + pageSize - 1;
                            List<getproduct> getsp = tvprd.GetAllProductVariation(pageSize, startIndex);
                            var result1 = getsp.Where(x => x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price    | Quantity |");
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            foreach (getproduct ht in result1)
                            {
                                Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            }
                            Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                        }
                    }
                    else if (input.ToLower() == "p")
                    {
                        if (pageNumber > 1)
                        {
                            pageNumber--;
                            startIndex = (pageNumber - 1) * pageSize;
                            endIndex = startIndex + pageSize - 1;
                            List<getproduct> getspt = tvprd.GetAllProductVariation(pageSize, startIndex);
                            var result2 = getspt.Where(x => x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price    | Quantity |");
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            foreach (getproduct ht in result2)
                            {
                                Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            }
                            Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                        }
                        else
                        {
                            Console.WriteLine("   +------------------+");
                            Console.WriteLine("-->|This Is First Page|<--");
                            Console.WriteLine("   +------------------+");
                        }
                    }
                    else if (input.ToLower() == "q")
                    {
                        exit = true;
                        Console.WriteLine("Exited");
                    }
                    else
                    {
                        if (int.TryParse(input, out product_ma))
                        {
                            int a = pageNumber;
                            int b = (a - 1) * pageSize;
                            int c = b + pageSize - 1;
                            List<getproduct> q = tvprd.GetAllProductVariation(pageSize, b);
                            var getma = q.Where(x => x.product_id == product_ma && x.product_TrangThai == "on");
                            if (getma.Count() == 0)
                            {
                                Console.WriteLine("Not Found");
                            }
                            else
                            {
                                var danhsachbienthe = lstvariation.Where(x => x.product_id == product_ma && x.variation_TrangThai == "on");
                                Console.WriteLine("+----+------------+--------+----------+--------+");
                                Console.WriteLine("| ID | Color      | Size   | Quantity | Status |");
                                Console.WriteLine("+----+------------+--------+----------+--------+");
                                foreach (variation ht in danhsachbienthe)
                                {
                                    Console.WriteLine("| {0,-3}| {1,-11}| {2,-7}| {3,-9}| {4,-7}|", ht.variation_id, ht.color_name, ht.size_name, ht.product_quantity, ht.variation_TrangThai);
                                    Console.WriteLine("+----+------------+--------+----------+--------+");
                                }
                                int lst = 0;
                                while (lst == 0)
                                {
                                    Console.Write("Enter Variation ID To Delete: ");
                                    mabienthe = int.Parse(Console.ReadLine());
                                    lst = danhsachbienthe.Where(x => x.variation_id == mabienthe).Count();
                                    if (lst == 0)
                                    {
                                        Console.WriteLine("Try Again");
                                    }
                                }
                                string yn = "";
                                do
                                {
                                    Console.Write("Are You Sure?(Y/N): ");
                                    yn = Console.ReadLine();
                                    if (yn.ToLower() != "n" && yn.ToLower() != "y")
                                    {
                                        Console.WriteLine("Try Again");
                                    }
                                    else if (yn.ToLower() == "y")
                                    {
                                        tvvariation.DeleteVariation(mabienthe);
                                        Console.WriteLine("Successful Deleted");
                                        exit = true;
                                    }
                                    else if (yn.ToLower() == "n")
                                    {
                                        exit = true;
                                    }
                                } while (yn.ToLower() != "n" && yn.ToLower() != "y");

                            }
                        }
                    }
                }
            }
        }
    }
}