using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.Controller
{
    public class CUDProduct
    {
        public void UpdateProduct()
        {
            try
            {
                int thulaidanhmuc;
                string category_ten;
                int product_ma = 0;
                int pageNumber = 1;
                int pageSize = 10;
                int startIndex = (pageNumber - 1) * pageSize;
                int endIndex = startIndex + pageSize - 1;
                bool exit = false;
                TVCategory tvcategory = new TVCategory();
                List<category> lstcategory = tvcategory.GetAllCategory();
                TVVariation addvariation = new TVVariation();
                TVProduct tvproduct = new TVProduct();
                TVProductVariation tvprd = new TVProductVariation();
                List<getproduct> getsps = tvprd.GetAllProductVariation(pageSize, startIndex);
                List<variation> vr = addvariation.GetAllVariation();
                var result = getsps;
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
                        Console.WriteLine("Enter Number Is Product ID to Update");
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
                                    Console.Write("Enter New Product Name: ");
                                    string product_ten = Console.ReadLine();
                                    Console.WriteLine("+-----------------+");
                                    Console.WriteLine("|  Category Name  |");
                                    Console.WriteLine("+-----------------+");
                                    foreach (category ca in lstcategory)
                                    {
                                        Console.WriteLine("| {0, -16}|", ca.category_name);
                                        Console.WriteLine("+-----------------+");
                                    }
                                    do
                                    {
                                        Console.Write("Enter New category name: ");
                                        category_ten = Console.ReadLine();
                                        var kiemtradanhmuc = lstcategory.Where(x => x.category_name == category_ten);
                                        thulaidanhmuc = kiemtradanhmuc.Count();
                                        if (thulaidanhmuc == 0)
                                        {
                                            Console.WriteLine("Not Found, Please Try Again");
                                        }
                                    } while (thulaidanhmuc == 0);
                                    float product_gia;
                                    do
                                    {
                                        Console.Write("Enter New Price: ");
                                        product_gia = float.Parse(Console.ReadLine());
                                        if (product_gia <= 0)
                                        {
                                            Console.WriteLine("Price must greater 0");
                                        }
                                    } while (product_gia <= 0);
                                    string product_tgth = "";
                                    do
                                    {
                                        Console.Write("Enter Status('on'/'off'): ");
                                        product_tgth = Console.ReadLine();
                                        if (product_tgth != "on" && product_tgth != "off")
                                        {
                                            Console.WriteLine("Try Again");
                                        }
                                    } while (product_tgth != "on" && product_tgth != "off");
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
                                            tvproduct.UpdateProduct(product_ma, product_ten, category_ten, product_gia, product_tgth);
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
        public void DeleteProduct()
        {
            try
            {
                int thulaidanhmuc;
                string category_ten;
                int product_ma = 0;
                int pageNumber = 1;
                int pageSize = 10;
                int startIndex = (pageNumber - 1) * pageSize;
                int endIndex = startIndex + pageSize - 1;
                bool exit = false;
                TVProduct tvproduct = new TVProduct();
                TVProductVariation tvprd = new TVProductVariation();
                List<getproduct> getsps = tvprd.GetAllProductVariation(pageSize, startIndex);
                var result = getsps.Where(x => x.product_TrangThai == "on");
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
                                            tvproduct.DeleteProduct(product_ma);
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
                Console.WriteLine("Error " + ex);
            }
        }
    }
}