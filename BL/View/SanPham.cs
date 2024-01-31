using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.View
{
    public class SanPham
    {
        public void HienThiAllSP()
        {
            int pageNumber = 1;
            int pageSize = 10;
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = startIndex + pageSize - 1;
            bool exit = false;
            TVProductVariation tvprd = new TVProductVariation();
            List<getproduct> getsps = tvprd.GetAllProductVariation(pageSize, startIndex);
            var result = getsps.Where(x => x.product_TrangThai == "on" && x.variation_TrangThai == "on");
            List<getproduct> dem1 = tvprd.GetAllHienthi();
            var dem = dem1.Where(x => x.product_TrangThai == "on");
            int count = dem.Count();
            int totalProducts = count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            if (result.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
                Console.Write("Enter Any Key To Exit...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                Console.WriteLine("| ID | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                foreach (getproduct ht in result)
                {
                    Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                }
                Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                while (!exit)
                {
                    Console.Write("Enter 'N' To Next Page, 'P' To Back Page, Or 'Q' To Exit: ");
                    string input = Console.ReadLine();
                    switch (input.ToLower())
                    {
                        case "n":
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
                                    Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
                                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                    foreach (getproduct ht in result1)
                                    {
                                        Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                        Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                    }
                                    Console.WriteLine("                                                                                                 ({0}/{1})", pageNumber, totalPages);
                                }
                                break;
                            }
                        case "p":
                            {
                                if (pageNumber > 1)
                                {
                                    pageNumber--;
                                    startIndex = (pageNumber - 1) * pageSize;
                                    endIndex = startIndex + pageSize - 1;
                                    List<getproduct> getspt = tvprd.GetAllProductVariation(pageSize, startIndex);
                                    var result2 = getspt.Where(x => x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                                    Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
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
                                break;
                            }
                        case "q":
                            {
                                exit = true;
                                Console.WriteLine("Exited");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Choice Again");
                                break;
                            }
                    }
                }
            }
        }
        public void SearchByCategoryName(string value, string product_onoff, string variation_onoff)
        {
            TVProductVariation prd = new TVProductVariation();
            List<getproduct> timsp = prd.GetAllHienthi();
            var result = timsp.Where(x => x.category_name.ToLower().Contains(value.ToLower()) && x.product_TrangThai == product_onoff && x.variation_TrangThai == variation_onoff);
            if (timsp.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                if (result.Count() == 0)
                {
                    Console.WriteLine("Not Found");
                }
                else
                {
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    foreach (getproduct ht in result)
                    {
                        Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                        Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    }
                }
            }
        }
        public void SearchByProductName(string value, string product_onoff, string variation_onoff)
        {
            TVProductVariation prd = new TVProductVariation();
            List<getproduct> timsp = prd.GetAllHienthi();
            var result = timsp.Where(x => x.product_name.ToLower().Contains(value.ToLower()) && x.product_TrangThai == product_onoff && x.variation_TrangThai == variation_onoff);
            if (timsp.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                if (result.Count() == 0)
                {
                    Console.WriteLine("Not Found");
                }
                else
                {
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    foreach (getproduct ht in result)
                    {
                        Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                        Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    }
                }
            }
        }
        public void SearchByPrice(float valuemax, float valuemin, string product_onoff, string variation_onoff)
        {
            TVProductVariation prdpr = new TVProductVariation();
            List<getproduct> timgiasp = prdpr.GetAllHienthi();
            var result = timgiasp.Where(x => x.product_price >= valuemin && x.product_price <= valuemax && x.product_TrangThai == product_onoff && x.variation_TrangThai == variation_onoff);
            if (timgiasp.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                if (result.Count() == 0)
                {
                    Console.WriteLine("Not Found");
                }
                else
                {
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    foreach (getproduct ht in result)
                    {
                        Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                        Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    }
                }
            }
        }
    }
}