using System.Security.Cryptography;
using System.Data;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.View;
using DAL.DBContext;
using DAL.Services;
using System.Text.RegularExpressions;

namespace BL.Controller
{
    public class CUDOrder
    {
        public void GetByShowProduct(string user)
        {
            List<getthongtinorder> lstgetthongtinorder = new List<getthongtinorder>();
            int ktdcm = 0;
            TVOrderPay tvodp = new TVOrderPay();
            int nhapmasp = 0, nhapmabt = 0, nhapdc = 0;
            int pageNumber = 1;
            int pageSize = 10;
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = startIndex + pageSize - 1;
            TVDeliveryAddress tvdc = new TVDeliveryAddress();
            TVPay tvpay = new TVPay();
            TVOder tvod = new TVOder();
            TVVariation tv = new TVVariation();
            TVProductVariation tvprd = new TVProductVariation();
            List<getproduct> getsps = tvprd.GetAllProductVariation(pageSize, startIndex);
            List<variation> vr = tv.GetAllVariation();
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
                string cn = "";
                do
                {
                    int chonbienthe = 1;
                    int chondc = 1;
                    int chonpay = 1;
                    int chonsolg = 1;
                    bool exit = false;
                    Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                    Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
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
                        Console.WriteLine("Enter Number Is Product ID to Order");
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
                                Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
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
                        }
                        else if (input.ToLower() == "q")
                        {
                            exit = true;
                            Console.WriteLine("Exited");
                        }
                        else
                        {
                            if (int.TryParse(input, out nhapmasp))
                            {
                                int a = pageNumber;
                                int b = (a - 1) * pageSize;
                                int c = b + pageSize - 1;
                                List<getproduct> q = tvprd.GetAllProductVariation(pageSize, b);
                                var getma = q.Where(x => x.product_id == nhapmasp && x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                                var bt = vr.Where(x => x.product_id == nhapmasp && x.variation_TrangThai == "on");
                                if (getma.Count() == 0)
                                {
                                    Console.WriteLine("Not Found");
                                }
                                else
                                {
                                    Console.WriteLine("+----+------------+--------+----------+");
                                    Console.WriteLine("| ID | Color      | Size   | Quantity |");
                                    Console.WriteLine("+----+------------+--------+----------+");
                                    foreach (variation ht in bt)
                                    {
                                        Console.WriteLine("| {0,-3}| {1,-11}| {2,-7}| {3,-9}|", ht.variation_id, ht.color_name, ht.size_name, ht.product_quantity);
                                        Console.WriteLine("+----+------------+--------+----------+");
                                    }
                                    do
                                    {
                                        Console.Write("Enter The Variatino ID: ");
                                        nhapmabt = int.Parse(Console.ReadLine());
                                        var chonbt = vr.Where(x => x.product_id == nhapmasp && x.variation_id == nhapmabt && x.variation_TrangThai == "on");
                                        if (chonbt.Count() == 0)
                                        {
                                            Console.WriteLine("Not Found");
                                            Console.WriteLine("Please Try Again");
                                            do
                                            {
                                                Console.WriteLine("Do You Want To Try Again?");
                                                Console.WriteLine("0. Exit");
                                                Console.WriteLine("1. Re-Enter");
                                                Console.Write("--> ");
                                                chonbienthe = int.Parse(Console.ReadLine());
                                                if (chonbienthe != 0 && chonbienthe != 1)
                                                {
                                                    Console.WriteLine("Try Again");
                                                }
                                            } while (chonbienthe != 0 && chonbienthe != 1);
                                            if (chonbienthe == 0)
                                            {
                                                chonsolg = 0;
                                                chonbienthe = 0;
                                                chondc = 0;
                                                chonpay = 0;
                                                exit = true;
                                                cn = "n";
                                            }
                                        }
                                        else
                                        {
                                            while (chonsolg != 0)
                                            {
                                                Console.Write("Enter The Quantity You Want To Order: ");
                                                int slg = int.Parse(Console.ReadLine());
                                                int ktslg = vr.Where(x => x.product_id == nhapmasp && x.variation_id == nhapmabt).Select(x => x.product_quantity).FirstOrDefault();
                                                if (ktslg - slg < 0 || slg <= 0)
                                                {
                                                    Console.WriteLine("The Number Of Products You Purchase Must By Less Than Or Equal To The Number Of Products Currently Available");
                                                    do
                                                    {
                                                        Console.WriteLine("Do You Want To Exit?");
                                                        Console.WriteLine("0. Exit");
                                                        Console.WriteLine("1. Re-Enter");
                                                        Console.Write("--> ");
                                                        chonsolg = int.Parse(Console.ReadLine());
                                                        if (chonsolg != 0 && chonsolg != 1)
                                                        {
                                                            Console.WriteLine("Try Again");
                                                        }
                                                    } while (chonsolg != 0 && chonsolg != 1);
                                                    if (chonsolg == 0)
                                                    {
                                                        chonsolg = 0;
                                                        chonbienthe = 0;
                                                        chondc = 0;
                                                        chonpay = 0;
                                                        exit = true;
                                                        cn = "n";
                                                    }
                                                }
                                                else
                                                {
                                                    var giasp = d.Where(x => x.product_id == nhapmasp).Select(x => x.product_price).FirstOrDefault();
                                                    var gia = giasp * slg;
                                                    int lastInsertId = tvod.AddOder(user, nhapmasp, nhapmabt, slg, gia);
                                                    getthongtinorder gettt = new getthongtinorder()
                                                    {
                                                        order_id = lastInsertId,
                                                        product_id = nhapmasp,
                                                        variation_id = nhapmabt,
                                                        order_quantity = slg,
                                                        order_price = gia
                                                    };
                                                    lstgetthongtinorder.Add(gettt);
                                                    do
                                                    {
                                                        Console.Write("Do You Want To Continue?(Y/N): ");
                                                        cn = Console.ReadLine();
                                                        if (cn.ToLower() != "n" && cn.ToLower() != "y")
                                                        {
                                                            Console.WriteLine("Try Again");
                                                        }
                                                        else if (cn.ToLower() == "y")
                                                        {
                                                            chonsolg = 0;
                                                            chonbienthe = 0;
                                                            chondc = 0;
                                                            chonpay = 0;
                                                            exit = true;
                                                            cn = "y";
                                                        }
                                                        else if (cn.ToLower() == "n")
                                                        {
                                                            float giaphaitra = 0;
                                                            foreach (getthongtinorder gtt in lstgetthongtinorder)
                                                            {
                                                                giaphaitra += gtt.order_price;
                                                            }
                                                            Console.WriteLine("You Need To Pay The Store The Amount Of: " + giaphaitra);
                                                            List<getpay> lstgetpay = tvpay.GetPayForCustomer(user);
                                                            Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            Console.WriteLine("| ID | Pay Name      | Bank Name     | Full Bank Name                                      |");
                                                            Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            foreach (getpay p in lstgetpay)
                                                            {
                                                                Console.WriteLine("| {0,-3}| {0,-14}| {1,-14}| {2,-52}|", p.pay_id, p.pay_name, p.pay_name_vt, p.pay_name_dd);
                                                                Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            }
                                                            while (chonpay != 0)
                                                            {
                                                                Console.Write("Please Enter Your Payment Method: ");
                                                                int thanhtoan = int.Parse(Console.ReadLine());
                                                                var ktmathanhtoan = lstgetpay.Find(x => x.pay_id == thanhtoan);
                                                                if (ktmathanhtoan == null)
                                                                {
                                                                    Console.WriteLine("Not Found, Please Try Again");
                                                                }
                                                                else
                                                                {
                                                                    List<delivery_address> lstaddress = tvdc.GetAllDeliveryAddress(user);
                                                                    var getaddress = lstaddress.Where(x => x.delivery_address_TrangThai == "on");
                                                                    if (getaddress.Count() == 0)
                                                                    {
                                                                        CUDDeliveryAddress themdiachi = new CUDDeliveryAddress();
                                                                        themdiachi.Add(user);
                                                                        ktdcm = 1;
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        Console.WriteLine("| ID | Consignee Name     | Consignee Phonenumber | Consignee Address                                                       |");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        foreach (delivery_address ad in getaddress)
                                                                        {
                                                                            Console.WriteLine("| {0, -3}| {1, -19}| {2, -22}| {3, -72}|", ad.delivery_address_id, ad.consignee_name, ad.consignee_phonenumber, ad.fulladdress);
                                                                            Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        }
                                                                    }
                                                                    if (ktdcm == 1)
                                                                    {
                                                                        tvdc = new TVDeliveryAddress();
                                                                        lstaddress = tvdc.GetAllDeliveryAddress(user);
                                                                        getaddress = lstaddress.Where(x => x.delivery_address_TrangThai == "on");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        Console.WriteLine("| ID | Consignee Name     | Consignee Phonenumber | Consignee Address                                                       |");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        foreach (delivery_address ad in getaddress)
                                                                        {
                                                                            Console.WriteLine("| {0, -3}| {1, -19}| {2, -22}| {3, -72}|", ad.delivery_address_id, ad.consignee_name, ad.consignee_phonenumber, ad.fulladdress);
                                                                            Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        }
                                                                    }
                                                                    while (chondc != 0)
                                                                    {
                                                                        Console.Write("Enter The Delivery Address ID: ");
                                                                        nhapdc = int.Parse(Console.ReadLine());
                                                                        var ktdc = lstaddress.Where(x => x.delivery_address_id == nhapdc && x.delivery_address_TrangThai == "on");
                                                                        if (ktdc.Count() == 0)
                                                                        {
                                                                            Console.WriteLine("Not Found, Please Try Again");
                                                                            chondc = 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            var YN = "";
                                                                            do
                                                                            {
                                                                                Console.Write("Are You Sure?(Y/N): ");
                                                                                YN = Console.ReadLine();
                                                                                if (YN.ToLower() == "y")
                                                                                {
                                                                                    foreach (getthongtinorder gttod in lstgetthongtinorder)
                                                                                    {
                                                                                        tvodp.AddOrderPay(gttod.order_id, thanhtoan, nhapdc);
                                                                                    }
                                                                                    chonsolg = 0;
                                                                                    chonbienthe = 0;
                                                                                    chondc = 0;
                                                                                    chonpay = 0;
                                                                                    exit = true;
                                                                                    cn = "n";
                                                                                }
                                                                                else if (YN.ToLower() != "n" && YN.ToLower() != "y")
                                                                                {
                                                                                    Console.WriteLine("Try Again");
                                                                                }
                                                                                else if (YN.ToLower() == "n")
                                                                                {
                                                                                    TVOrderPayAddress tvopa = new TVOrderPayAddress();
                                                                                    foreach (getthongtinorder g in lstgetthongtinorder)
                                                                                    {
                                                                                        tvopa.DeleteOrderPayAddress(g.order_id);
                                                                                    }
                                                                                    chonsolg = 0;
                                                                                    chonbienthe = 0;
                                                                                    chondc = 0;
                                                                                    chonpay = 0;
                                                                                    exit = true;
                                                                                    cn = "n";
                                                                                }
                                                                            } while (YN.ToLower() != "n" && YN.ToLower() != "y");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    } while (cn.ToLower() != "n" && cn.ToLower() != "y");
                                                }
                                            }
                                        }
                                    } while (chonbienthe != 0);
                                }
                            }
                        }
                    }
                } while (cn.ToLower() == "y");
            }

        }
        public void GetByProduct(string user)
        {
            try
            {
                int masanpham;
                int mabienthe;
                int madiachi;
                int soluong;
                int kiemtradiachi = 0;
                TVOder tvoder = new TVOder();
                TVOrderPay tvorderpay = new TVOrderPay();
                TVProductVariation tvproductvariation = new TVProductVariation();
                List<getproduct> lstproduct = tvproductvariation.GetAllHienthi();
                TVVariation tvvariation = new TVVariation();
                List<variation> lstvariation = tvvariation.GetAllVariation();
                TVPay tvpay = new TVPay();
                List<payment> lstpayment = tvpay.GetPayment();
                TVDeliveryAddress tvdeliveryaddress = new TVDeliveryAddress();
                List<delivery_address> lstaddress = tvdeliveryaddress.GetAllDeliveryAddress(user);
                List<getthongtinorder> lstgetthongtinorder = new List<getthongtinorder>();
                string cn = "";
                do
                {

                    int chontensanpham = 1;
                    int chonsanpham = 1;
                    int chondc = 1;
                    int chonsolg = 1;
                    int chonbienthe = 1;
                    int chonpay = 1;
                    do
                    {
                        Console.Write("Enter The Product Name: ");

                        string tensanpham = Console.ReadLine();
                        var listtimkiem = lstproduct.Where(x => x.product_name.ToLower().Contains(tensanpham.ToLower()) && x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                        if (listtimkiem.Count() == 0)
                        {
                            Console.WriteLine("Not Found");
                            do
                            {
                                Console.WriteLine("Do You Want To Exit?");
                                Console.WriteLine("0. Exit");
                                Console.WriteLine("1. Re-Enter");
                                Console.Write("--> ");
                                chontensanpham = int.Parse(Console.ReadLine());
                                if (chontensanpham != 0 && chontensanpham != 1)
                                {
                                    Console.WriteLine("Try Again");
                                }
                            } while (chontensanpham != 0 && chontensanpham != 1);
                            if (chontensanpham == 0)
                            {
                                chontensanpham = 0;
                                chondc = 0;
                                chonsolg = 0;
                                chonbienthe = 0;
                                chonpay = 0;
                                chonsanpham = 0;
                                cn = "n";
                            }
                        }
                        else
                        {
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            foreach (getproduct ht in listtimkiem)
                            {
                                Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            }
                            while (chonsanpham != 0)
                            {
                                Console.Write("Enter The Product ID You Want To Order: ");
                                masanpham = int.Parse(Console.ReadLine());
                                var ktmasanpham = listtimkiem.Where(x => x.product_id == masanpham);
                                if (ktmasanpham.Count() == 0)
                                {
                                    Console.WriteLine("Not Found");
                                    do
                                    {
                                        Console.WriteLine("Do You Want To Exit?");
                                        Console.WriteLine("0. Exit");
                                        Console.WriteLine("1. Re-Enter");
                                        Console.Write("--> ");
                                        chonsanpham = int.Parse(Console.ReadLine());
                                        if (chonsanpham != 0 && chonsanpham != 1)
                                        {
                                            Console.WriteLine("Try Again");
                                        }
                                    } while (chonsanpham != 0 && chonsanpham != 1);
                                    if (chonsanpham == 0)
                                    {
                                        chontensanpham = 0;
                                        chondc = 0;
                                        chonsolg = 0;
                                        chonbienthe = 0;
                                        chonpay = 0;
                                        chonsanpham = 0;
                                        cn = "n";
                                    }
                                }
                                else
                                {
                                    var listhienthibienthe = lstvariation.Where(x => x.product_id == masanpham && x.variation_TrangThai == "on");
                                    Console.WriteLine("+----+------------+--------+----------+");
                                    Console.WriteLine("| ID | Color      | Size   | Quantity |");
                                    Console.WriteLine("+----+------------+--------+----------+");
                                    foreach (variation ht in listhienthibienthe)
                                    {
                                        Console.WriteLine("| {0,-3}| {1,-11}| {2,-7}| {3,-9}|", ht.variation_id, ht.color_name, ht.size_name, ht.product_quantity);
                                        Console.WriteLine("+----+------------+--------+----------+");
                                    }
                                    while (chonbienthe != 0)
                                    {
                                        Console.Write("Enter The Variation ID You Want To Order: ");
                                        mabienthe = int.Parse(Console.ReadLine());
                                        var ktmabienthe = listhienthibienthe.Where(x => x.variation_id == mabienthe);
                                        if (ktmabienthe.Count() == 0)
                                        {
                                            Console.WriteLine("Not Found");
                                            do
                                            {
                                                Console.WriteLine("Do You Want To Exit?");
                                                Console.WriteLine("0. Exit");
                                                Console.WriteLine("1. Re-Enter");
                                                Console.Write("--> ");
                                                chonbienthe = int.Parse(Console.ReadLine());
                                                if (chonbienthe != 0 && chonbienthe != 1)
                                                {
                                                    Console.WriteLine("Try Again");
                                                }
                                            } while (chonbienthe != 0 && chonbienthe != 1);
                                            if (chonbienthe == 0)
                                            {
                                                chontensanpham = 0;
                                                chondc = 0;
                                                chonsolg = 0;
                                                chonbienthe = 0;
                                                chonpay = 0;
                                                chonsanpham = 0;
                                                cn = "n";
                                            }
                                        }
                                        else
                                        {
                                            int soluongdangco = ktmabienthe.Where(x => x.variation_id == mabienthe).Select(x => x.product_quantity).FirstOrDefault();
                                            while (chonsolg != 0)
                                            {
                                                Console.Write("Enter The Quantity You Want To Order: ");
                                                soluong = int.Parse(Console.ReadLine());
                                                if (soluong > soluongdangco && soluong <= 0)
                                                {
                                                    Console.WriteLine("The Number Of Products You Purchase Must By Less Than Or Equal To The Number Of Products Currently Available");
                                                    do
                                                    {
                                                        Console.WriteLine("Do You Want To Exit?");
                                                        Console.WriteLine("0. Exit");
                                                        Console.WriteLine("1. Re-Enter");
                                                        Console.Write("--> ");
                                                        chonsolg = int.Parse(Console.ReadLine());
                                                        if (chonsolg != 0 && chonsolg != 1)
                                                        {
                                                            Console.WriteLine("Try Again");
                                                        }
                                                    } while (chonsolg != 0 && chonsolg != 1);
                                                    if (chonsolg == 0)
                                                    {
                                                        chontensanpham = 0;
                                                        chondc = 0;
                                                        chonsolg = 0;
                                                        chonbienthe = 0;
                                                        chonpay = 0;
                                                        chonsanpham = 0;
                                                        cn = "n";
                                                    }
                                                }
                                                else
                                                {
                                                    var giasp = lstproduct.Where(x => x.product_id == masanpham).Select(x => x.product_price).FirstOrDefault();
                                                    var gia = giasp * soluong;
                                                    int lastInsertId = tvoder.AddOder(user, masanpham, mabienthe, soluong, gia);
                                                    getthongtinorder gettt = new getthongtinorder()
                                                    {
                                                        order_id = lastInsertId,
                                                        product_id = masanpham,
                                                        variation_id = mabienthe,
                                                        order_quantity = soluong,
                                                        order_price = gia
                                                    };
                                                    lstgetthongtinorder.Add(gettt);
                                                    do
                                                    {
                                                        Console.Write("Do You Want To Continue?(Y/N): ");
                                                        cn = Console.ReadLine();
                                                        if (cn.ToLower() == "y")
                                                        {
                                                            chontensanpham = 0;
                                                            chondc = 0;
                                                            chonsolg = 0;
                                                            chonbienthe = 0;
                                                            chonpay = 0;
                                                            chonsanpham = 0;
                                                            cn = "y";
                                                        }
                                                        else if (cn.ToLower() != "n" && cn.ToLower() != "y")
                                                        {
                                                            Console.WriteLine("Try Again");
                                                        }
                                                        else if (cn.ToLower() == "n")
                                                        {
                                                            float giaphaitra = 0;
                                                            foreach (getthongtinorder gtt in lstgetthongtinorder)
                                                            {
                                                                giaphaitra += gtt.order_price;
                                                            }
                                                            Console.WriteLine("You Need To Pay The Store The Amount Of: " + giaphaitra);
                                                            List<getpay> lstgetpay = tvpay.GetPayForCustomer(user);
                                                            Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            Console.WriteLine("| ID | Pay Name      | Bank Name     | Full Bank Name                                      |");
                                                            Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            foreach (getpay p in lstgetpay)
                                                            {
                                                                Console.WriteLine("| {0,-3}| {0,-14}| {1,-14}| {2,-52}|", p.pay_id, p.pay_name, p.pay_name_vt, p.pay_name_dd);
                                                                Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            }
                                                            while (chonpay != 0)
                                                            {
                                                                Console.Write("Please Enter Your Payment Method: ");
                                                                int thanhtoan = int.Parse(Console.ReadLine());
                                                                var ktmathanhtoan = lstgetpay.Find(x => x.pay_id == thanhtoan);
                                                                if (ktmathanhtoan == null)
                                                                {
                                                                    Console.WriteLine("Not Found, Please Try Again");
                                                                    chonpay = 1;
                                                                }
                                                                else
                                                                {
                                                                    List<delivery_address> lstaddressmoi = tvdeliveryaddress.GetAllDeliveryAddress(user);
                                                                    var getaddress = lstaddressmoi.Where(x => x.delivery_address_TrangThai == "on");
                                                                    if (getaddress.Count() == 0)
                                                                    {
                                                                        Console.WriteLine("List Your Delivery Address Is Empty");
                                                                        Console.WriteLine("Please Create A New Delivery Address");
                                                                        CUDDeliveryAddress themdiachi = new CUDDeliveryAddress();
                                                                        themdiachi.Add(user);
                                                                        kiemtradiachi = 1;
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        Console.WriteLine("| ID | Consignee Name     | Consignee Phonenumber | Consignee Address                                                       |");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        foreach (delivery_address ad in getaddress)
                                                                        {
                                                                            Console.WriteLine("| {0, -3}| {1, -19}| {2, -22}| {3, -72}|", ad.delivery_address_id, ad.consignee_name, ad.consignee_phonenumber, ad.fulladdress);
                                                                            Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        }
                                                                    }
                                                                    if (kiemtradiachi == 1)
                                                                    {
                                                                        tvdeliveryaddress = new TVDeliveryAddress();
                                                                        lstaddress = tvdeliveryaddress.GetAllDeliveryAddress(user);
                                                                        getaddress = lstaddress.Where(x => x.delivery_address_TrangThai == "on");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        Console.WriteLine("| ID | Consignee Name     | Consignee Phonenumber | Consignee Address                                                       |");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        foreach (delivery_address ad in getaddress)
                                                                        {
                                                                            Console.WriteLine("| {0, -3}| {1, -19}| {2, -22}| {3, -72}|", ad.delivery_address_id, ad.consignee_name, ad.consignee_phonenumber, ad.fulladdress);
                                                                            Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        }
                                                                    }
                                                                    while (chondc != 0)
                                                                    {
                                                                        Console.Write("Enter The Delivery Address ID: ");
                                                                        madiachi = int.Parse(Console.ReadLine());
                                                                        var ktdc = lstaddress.Where(x => x.delivery_address_id == madiachi && x.delivery_address_TrangThai == "on");
                                                                        if (ktdc.Count() == 0)
                                                                        {
                                                                            Console.WriteLine("Not Found, Please Try Again");
                                                                            chondc = 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            var YN = "";
                                                                            do
                                                                            {
                                                                                Console.Write("Are You Sure?(Y/N):");
                                                                                YN = Console.ReadLine();
                                                                                if (YN.ToLower() == "y")
                                                                                {
                                                                                    foreach (getthongtinorder gtt in lstgetthongtinorder)
                                                                                    {
                                                                                        tvorderpay.AddOrderPay(gtt.order_id, thanhtoan, madiachi);
                                                                                    }
                                                                                    chontensanpham = 0;
                                                                                    chondc = 0;
                                                                                    chonsolg = 0;
                                                                                    chonbienthe = 0;
                                                                                    chonpay = 0;
                                                                                    chonsanpham = 0;
                                                                                    cn = "n";
                                                                                }
                                                                                else if (YN.ToLower() != "n" && YN.ToLower() != "y")
                                                                                {
                                                                                    Console.WriteLine("Try Again");
                                                                                }
                                                                                else if (YN.ToLower() == "n")
                                                                                {
                                                                                    TVOrderPayAddress tvopa = new TVOrderPayAddress();
                                                                                    foreach (getthongtinorder g in lstgetthongtinorder)
                                                                                    {
                                                                                        tvopa.DeleteOrderPayAddress(g.order_id);
                                                                                    }
                                                                                    chontensanpham = 0;
                                                                                    chondc = 0;
                                                                                    chonsolg = 0;
                                                                                    chonbienthe = 0;
                                                                                    chonpay = 0;
                                                                                    chonsanpham = 0;
                                                                                    cn = "n";
                                                                                }
                                                                            } while (YN.ToLower() != "y" && YN.ToLower() != "n");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    } while (cn.ToLower() != "n" && cn.ToLower() != "y");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } while (chontensanpham != 0);
                } while (cn.ToLower() == "y");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void GetByCategory(string user)
        {
            try
            {
                int masanpham;
                int mabienthe;
                int madiachi;
                int soluong;
                int kiemtradiachi = 0;
                List<getthongtinorder> lstgetthongtinorder = new List<getthongtinorder>();
                TVOder tvoder = new TVOder();
                TVOrderPay tvorderpay = new TVOrderPay();
                TVProductVariation tvproductvariation = new TVProductVariation();
                List<getproduct> lstproduct = tvproductvariation.GetAllHienthi();
                TVVariation tvvariation = new TVVariation();
                List<variation> lstvariation = tvvariation.GetAllVariation();
                TVPay tvpay = new TVPay();
                List<payment> lstpayment = tvpay.GetPayment();
                TVDeliveryAddress tvdeliveryaddress = new TVDeliveryAddress();
                List<delivery_address> lstaddress = tvdeliveryaddress.GetAllDeliveryAddress(user);
                string cn = "";
                do
                {

                    int chontendanhmuc = 1;
                    int chonsanpham = 1;
                    int chondc = 1;
                    int chonsolg = 1;
                    int chonbienthe = 1;
                    int chonpay = 1;
                    do
                    {
                        Console.Write("Enter The Category Name: ");
                        string tendanhmuc = Console.ReadLine();
                        var listtimkiem = lstproduct.Where(x => x.category_name.ToLower().Contains(tendanhmuc.ToLower()) && x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                        if (listtimkiem.Count() == 0)
                        {
                            Console.WriteLine("Not Found");
                            do
                            {
                                Console.WriteLine("Do You Want To Exit?");
                                Console.WriteLine("0. Exit");
                                Console.WriteLine("1. Re-Enter");
                                Console.Write("--> ");
                                chontendanhmuc = int.Parse(Console.ReadLine());
                                if (chontendanhmuc != 0 && chontendanhmuc != 1)
                                {
                                    Console.WriteLine("Try Again");
                                }
                            } while (chontendanhmuc != 0 && chontendanhmuc != 1);
                            if (chontendanhmuc == 0)
                            {
                                chontendanhmuc = 0;
                                chondc = 0;
                                chonsolg = 0;
                                chonbienthe = 0;
                                chonpay = 0;
                                chonsanpham = 0;
                            }
                        }
                        else
                        {
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            foreach (getproduct ht in listtimkiem)
                            {
                                Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            }
                            while (chonsanpham != 0)
                            {
                                Console.Write("Enter The Product ID You Want To Order: ");
                                masanpham = int.Parse(Console.ReadLine());
                                var ktmasanpham = listtimkiem.Where(x => x.product_id == masanpham);
                                if (ktmasanpham.Count() == 0)
                                {
                                    Console.WriteLine("Not Found");
                                    do
                                    {
                                        Console.WriteLine("Do You Want To Exit?");
                                        Console.WriteLine("0. Exit");
                                        Console.WriteLine("1. Re-Enter");
                                        Console.Write("--> ");
                                        chonsanpham = int.Parse(Console.ReadLine());
                                        if (chonsanpham != 0 && chonsanpham != 1)
                                        {
                                            Console.WriteLine("Try Again");
                                        }
                                    } while (chonsanpham != 0 && chonsanpham != 1);
                                    if (chonsanpham == 0)
                                    {
                                        chontendanhmuc = 0;
                                        chondc = 0;
                                        chonsolg = 0;
                                        chonbienthe = 0;
                                        chonpay = 0;
                                        chonsanpham = 0;
                                    }
                                }
                                else
                                {
                                    var listhienthibienthe = lstvariation.Where(x => x.product_id == masanpham && x.variation_TrangThai == "on");
                                    Console.WriteLine("+----+------------+--------+----------+");
                                    Console.WriteLine("| ID | Color      | Size   | Quantity |");
                                    Console.WriteLine("+----+------------+--------+----------+");
                                    foreach (variation ht in listhienthibienthe)
                                    {
                                        Console.WriteLine("| {0,-3}| {1,-11}| {2,-7}| {3,-9}|", ht.variation_id, ht.color_name, ht.size_name, ht.product_quantity, ht.variation_TrangThai);
                                        Console.WriteLine("+----+------------+--------+----------+");
                                    }
                                    while (chonbienthe != 0)
                                    {
                                        Console.Write("Enter The Variation ID You Want To Order: ");
                                        mabienthe = int.Parse(Console.ReadLine());
                                        var ktmabienthe = listhienthibienthe.Where(x => x.variation_id == mabienthe);
                                        if (ktmabienthe.Count() == 0)
                                        {
                                            Console.WriteLine("Not Found");
                                            do
                                            {
                                                Console.WriteLine("Do You Want To Exit?");
                                                Console.WriteLine("0. Exit");
                                                Console.WriteLine("1. Re-Enter");
                                                Console.Write("--> ");
                                                chonbienthe = int.Parse(Console.ReadLine());
                                                if (chonbienthe != 0 && chonbienthe != 1)
                                                {
                                                    Console.WriteLine("Try Again");
                                                }
                                            } while (chonbienthe != 0 && chonbienthe != 1);
                                            if (chonbienthe == 0)
                                            {
                                                chontendanhmuc = 0;
                                                chondc = 0;
                                                chonsolg = 0;
                                                chonbienthe = 0;
                                                chonpay = 0;
                                                chonsanpham = 0;
                                            }
                                        }
                                        else
                                        {
                                            int soluongdangco = ktmabienthe.Where(x => x.variation_id == mabienthe).Select(x => x.product_quantity).FirstOrDefault();
                                            while (chonsolg != 0)
                                            {
                                                Console.Write("Enter The Quantity You Want To Order: ");
                                                soluong = int.Parse(Console.ReadLine());
                                                if (soluong > soluongdangco || soluong <= 0)
                                                {
                                                    Console.WriteLine("The Number Of Products You Purchase Must By Less Than Or Equal To The Number Of Products Currently Available");
                                                    do
                                                    {
                                                        Console.WriteLine("Do You Want To Exit?");
                                                        Console.WriteLine("0. Exit");
                                                        Console.WriteLine("1. Re-Enter");
                                                        Console.Write("--> ");
                                                        chonsolg = int.Parse(Console.ReadLine());
                                                        if (chonsolg != 0 && chonsolg != 1)
                                                        {
                                                            Console.WriteLine("Try Again");
                                                        }
                                                    } while (chonsolg != 0 && chonsolg != 1);
                                                    if (chonsolg == 0)
                                                    {
                                                        chontendanhmuc = 0;
                                                        chondc = 0;
                                                        chonsolg = 0;
                                                        chonbienthe = 0;
                                                        chonpay = 0;
                                                        chonsanpham = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    var giasp = lstproduct.Where(x => x.product_id == masanpham).Select(x => x.product_price).FirstOrDefault();
                                                    var gia = giasp * soluong;
                                                    int lastInsertId = tvoder.AddOder(user, masanpham, mabienthe, soluong, gia);
                                                    getthongtinorder gettt = new getthongtinorder()
                                                    {
                                                        order_id = lastInsertId,
                                                        product_id = masanpham,
                                                        variation_id = mabienthe,
                                                        order_quantity = soluong,
                                                        order_price = gia
                                                    };
                                                    lstgetthongtinorder.Add(gettt);
                                                    do
                                                    {
                                                        Console.Write("Do You Want To Continue?(Y/N): ");
                                                        cn = Console.ReadLine();
                                                        if (cn.ToLower() == "y")
                                                        {
                                                            chontendanhmuc = 0;
                                                            chondc = 0;
                                                            chonsolg = 0;
                                                            chonbienthe = 0;
                                                            chonpay = 0;
                                                            chonsanpham = 0;
                                                            cn = "y";
                                                        }
                                                        else if (cn.ToLower() != "n" && cn.ToLower() != "y")
                                                        {
                                                            Console.WriteLine("Try Again");
                                                        }
                                                        else if (cn.ToLower() == "n")
                                                        {
                                                            float giaphaitra = 0;
                                                            foreach (getthongtinorder gtt in lstgetthongtinorder)
                                                            {
                                                                giaphaitra += gtt.order_price;
                                                            }
                                                            Console.WriteLine("You Need To Pay The Store The Amount Of: " + giaphaitra);
                                                            List<getpay> lstgetpay = tvpay.GetPayForCustomer(user);
                                                            Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            Console.WriteLine("| ID | Pay Name      | Bank Name     | Full Bank Name                                      |");
                                                            Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            foreach (getpay p in lstgetpay)
                                                            {
                                                                Console.WriteLine("| {0,-3}| {0,-14}| {1,-14}| {2,-52}|", p.pay_id, p.pay_name, p.pay_name_vt, p.pay_name_dd);
                                                                Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            }
                                                            while (chonpay != 0)
                                                            {
                                                                Console.Write("Please Enter Your Payment Method: ");
                                                                int thanhtoan = int.Parse(Console.ReadLine());
                                                                var ktmathanhtoan = lstgetpay.Find(x => x.pay_id == thanhtoan);
                                                                if (ktmathanhtoan == null)
                                                                {
                                                                    Console.WriteLine("Not Found, Please Try Again");
                                                                    chonpay = 1;
                                                                }
                                                                else
                                                                {
                                                                    List<delivery_address> lstaddressmoi = tvdeliveryaddress.GetAllDeliveryAddress(user);
                                                                    var getaddress = lstaddressmoi.Where(x => x.delivery_address_TrangThai == "on");
                                                                    if (getaddress.Count() == 0)
                                                                    {
                                                                        Console.WriteLine("List Your Delivery Address Is Empty");
                                                                        Console.WriteLine("Please Create A New Delivery Address");
                                                                        CUDDeliveryAddress themdiachi = new CUDDeliveryAddress();
                                                                        themdiachi.Add(user);
                                                                        kiemtradiachi = 1;
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        Console.WriteLine("| ID | Consignee Name     | Consignee Phonenumber | Consignee Address                                                       |");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        foreach (delivery_address ad in getaddress)
                                                                        {
                                                                            Console.WriteLine("| {0, -3}| {1, -19}| {2, -22}| {3, -72}|", ad.delivery_address_id, ad.consignee_name, ad.consignee_phonenumber, ad.fulladdress);
                                                                            Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        }
                                                                    }
                                                                    if (kiemtradiachi == 1)
                                                                    {
                                                                        tvdeliveryaddress = new TVDeliveryAddress();
                                                                        lstaddress = tvdeliveryaddress.GetAllDeliveryAddress(user);
                                                                        getaddress = lstaddress.Where(x => x.delivery_address_TrangThai == "on");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        Console.WriteLine("| ID | Consignee Name     | Consignee Phonenumber | Consignee Address                                                       |");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        foreach (delivery_address ad in getaddress)
                                                                        {
                                                                            Console.WriteLine("| {0, -3}| {1, -19}| {2, -22}| {3, -72}|", ad.delivery_address_id, ad.consignee_name, ad.consignee_phonenumber, ad.fulladdress);
                                                                            Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        }
                                                                    }
                                                                    while (chondc != 0)
                                                                    {
                                                                        Console.Write("Enter The Delivery Address ID: ");
                                                                        madiachi = int.Parse(Console.ReadLine());
                                                                        var ktdc = lstaddress.Where(x => x.delivery_address_id == madiachi && x.delivery_address_TrangThai == "on");
                                                                        if (ktdc.Count() == 0)
                                                                        {
                                                                            Console.WriteLine("Not Found, Please Try Again");
                                                                            chondc = 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            var YN = "";
                                                                            do
                                                                            {
                                                                                Console.Write("Are You Sure?(Y/N):");
                                                                                YN = Console.ReadLine();
                                                                                if (YN.ToLower() == "y")
                                                                                {
                                                                                    foreach (getthongtinorder gtt in lstgetthongtinorder)
                                                                                    {
                                                                                        tvorderpay.AddOrderPay(gtt.order_id, thanhtoan, madiachi);
                                                                                    }
                                                                                    chontendanhmuc = 0;
                                                                                    chondc = 0;
                                                                                    chonsolg = 0;
                                                                                    chonbienthe = 0;
                                                                                    chonpay = 0;
                                                                                    chonsanpham = 0;
                                                                                    cn = "n";
                                                                                }
                                                                                else if (YN.ToLower() != "n" && YN.ToLower() != "y")
                                                                                {
                                                                                    Console.WriteLine("Try Again");
                                                                                }
                                                                                else if (YN.ToLower() == "n")
                                                                                {
                                                                                    TVOrderPayAddress tvopa = new TVOrderPayAddress();
                                                                                    foreach (getthongtinorder g in lstgetthongtinorder)
                                                                                    {
                                                                                        tvopa.DeleteOrderPayAddress(g.order_id);
                                                                                    }
                                                                                    chontendanhmuc = 0;
                                                                                    chondc = 0;
                                                                                    chonsolg = 0;
                                                                                    chonbienthe = 0;
                                                                                    chonpay = 0;
                                                                                    chonsanpham = 0;
                                                                                    cn = "n";
                                                                                }
                                                                            } while (YN.ToLower() != "y" && YN.ToLower() != "n");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    } while (cn.ToLower() != "n" && cn.ToLower() != "y");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } while (chontendanhmuc != 0);
                } while (cn.ToLower() == "y");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void GetByPrice(string user)
        {
            try
            {
                int masanpham;
                int mabienthe;
                int madiachi;
                int soluong;
                int kiemtradiachi = 0;
                List<getthongtinorder> lstgetthongtinorder = new List<getthongtinorder>();
                TVOder tvoder = new TVOder();
                TVOrderPay tvorderpay = new TVOrderPay();
                TVProductVariation tvproductvariation = new TVProductVariation();
                List<getproduct> lstproduct = tvproductvariation.GetAllHienthi();
                TVVariation tvvariation = new TVVariation();
                List<variation> lstvariation = tvvariation.GetAllVariation();
                TVPay tvpay = new TVPay();
                List<payment> lstpayment = tvpay.GetPayment();
                TVDeliveryAddress tvdeliveryaddress = new TVDeliveryAddress();
                List<delivery_address> lstaddress = tvdeliveryaddress.GetAllDeliveryAddress(user);
                string cn = "";
                do
                {
                    int chongia = 1;
                    int chonsanpham = 1;
                    int chondc = 1;
                    int chonsolg = 1;
                    int chonbienthe = 1;
                    int chonpay = 1;
                    do
                    {
                        float pricemin, pricemax;
                        do
                        {
                            Console.Write("Enter Minimum Price: ");
                            pricemin = float.Parse(Console.ReadLine());
                            Console.Write("Enter Maximum Price: ");
                            pricemax = float.Parse(Console.ReadLine());
                            if (pricemax < pricemin)
                            {
                                Console.WriteLine("The Maximum Price Must Be Greater Than The Minimum Price Please Try Again");
                            }
                        } while (pricemax < pricemin);
                        var listtimkiem = lstproduct.Where(x => x.product_price >= pricemin && x.product_price <= pricemax && x.product_TrangThai == "on" && x.variation_TrangThai == "on");
                        if (listtimkiem.Count() == 0)
                        {
                            Console.WriteLine("Not Found");
                            do
                            {
                                Console.WriteLine("Do You Want To Exit?");
                                Console.WriteLine("0. Exit");
                                Console.WriteLine("1. Re-Enter");
                                Console.Write("--> ");
                                chongia = int.Parse(Console.ReadLine());
                                if (chongia != 0 && chongia != 1)
                                {
                                    Console.WriteLine("Try Again");
                                }
                            } while (chongia != 0 && chongia != 1);
                            if (chongia == 0)
                            {
                                chongia = 0;
                                chondc = 0;
                                chonsolg = 0;
                                chonbienthe = 0;
                                chonpay = 0;
                                chonsanpham = 0;
                                cn = "n";
                            }
                        }
                        else
                        {
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            Console.WriteLine("|ID  | Category Name | Product Name         | Color                 | Size         | Price($) | Quantity |");
                            Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            foreach (getproduct ht in listtimkiem)
                            {
                                Console.WriteLine("| {0,-3}| {1,-14}| {2,-21}| {3,-22}| {4,-13}| {5,-9}| {6,-9}|", ht.product_id, ht.category_name, ht.product_name, ht.color_name, ht.size_name, ht.product_price, ht.product_quantity);
                                Console.WriteLine("+----+---------------+----------------------+-----------------------+--------------+----------+----------+");
                            }
                            while (chonsanpham != 0)
                            {
                                Console.Write("Enter The Product ID You Want To Order: ");
                                masanpham = int.Parse(Console.ReadLine());
                                var ktmasanpham = listtimkiem.Where(x => x.product_id == masanpham);
                                if (ktmasanpham.Count() == 0)
                                {
                                    Console.WriteLine("Not Found");
                                    do
                                    {
                                        Console.WriteLine("Do You Want To Exit?");
                                        Console.WriteLine("0. Exit");
                                        Console.WriteLine("1. Re-Enter");
                                        Console.Write("--> ");
                                        chonsanpham = int.Parse(Console.ReadLine());
                                        if (chonsanpham != 0 && chonsanpham != 1)
                                        {
                                            Console.WriteLine("Try Again");
                                        }
                                    } while (chonsanpham != 0 && chonsanpham != 1);
                                    if (chonsanpham == 0)
                                    {
                                        chongia = 0;
                                        chondc = 0;
                                        chonsolg = 0;
                                        chonbienthe = 0;
                                        chonpay = 0;
                                        chonsanpham = 0;
                                        cn = "n";
                                    }
                                }
                                else
                                {
                                    var listhienthibienthe = lstvariation.Where(x => x.product_id == masanpham && x.variation_TrangThai == "on");
                                    Console.WriteLine("+----+------------+--------+----------+");
                                    Console.WriteLine("| ID | Color      | Size   | Quantity |");
                                    Console.WriteLine("+----+------------+--------+----------+");
                                    foreach (variation ht in listhienthibienthe)
                                    {
                                        Console.WriteLine("| {0,-3}| {1,-11}| {2,-7}| {3,-9}|", ht.variation_id, ht.color_name, ht.size_name, ht.product_quantity);
                                        Console.WriteLine("+----+------------+--------+----------+");
                                    }
                                    while (chonbienthe != 0)
                                    {
                                        Console.Write("Enter The Variation ID You Want To Order: ");
                                        mabienthe = int.Parse(Console.ReadLine());
                                        var ktmabienthe = listhienthibienthe.Where(x => x.variation_id == mabienthe);
                                        if (ktmabienthe.Count() == 0)
                                        {
                                            Console.WriteLine("Not Found");
                                            do
                                            {
                                                Console.WriteLine("Do You Want To Exit?");
                                                Console.WriteLine("0. Exit");
                                                Console.WriteLine("1. Re-Enter");
                                                Console.Write("--> ");
                                                chonbienthe = int.Parse(Console.ReadLine());
                                                if (chonbienthe != 0 && chonbienthe != 1)
                                                {
                                                    Console.WriteLine("Try Again");
                                                }
                                            } while (chonbienthe != 0 && chonbienthe != 1);
                                            if (chonbienthe == 0)
                                            {
                                                chongia = 0;
                                                chondc = 0;
                                                chonsolg = 0;
                                                chonbienthe = 0;
                                                chonpay = 0;
                                                chonsanpham = 0;
                                                cn = "n";
                                            }
                                        }
                                        else
                                        {
                                            int soluongdangco = ktmabienthe.Where(x => x.variation_id == mabienthe).Select(x => x.product_quantity).FirstOrDefault();
                                            while (chonsolg != 0)
                                            {
                                                Console.Write("Enter The Quantity You Want To Order: ");
                                                soluong = int.Parse(Console.ReadLine());
                                                if (soluong > soluongdangco || soluong <= 0)
                                                {
                                                    Console.WriteLine("The Number Of Products You Purchase Must By Less Than Or Equal To The Number Of Products Currently Available");
                                                    do
                                                    {
                                                        Console.WriteLine("Do You Want To Exit?");
                                                        Console.WriteLine("0. Exit");
                                                        Console.WriteLine("1. Re-Enter");
                                                        Console.Write("--> ");
                                                        chonsolg = int.Parse(Console.ReadLine());
                                                        if (chonsolg != 0 && chonsolg != 1)
                                                        {
                                                            Console.WriteLine("Try Again");
                                                        }
                                                    } while (chonsolg != 0 && chonsolg != 1);
                                                    if (chonsolg == 0)
                                                    {
                                                        chongia = 0;
                                                        chondc = 0;
                                                        chonsolg = 0;
                                                        chonbienthe = 0;
                                                        chonpay = 0;
                                                        chonsanpham = 0;
                                                        cn = "n";
                                                    }
                                                }
                                                else
                                                {
                                                    var giasp = lstproduct.Where(x => x.product_id == masanpham).Select(x => x.product_price).FirstOrDefault();
                                                    var gia = giasp * soluong;
                                                    int lastInsertId = tvoder.AddOder(user, masanpham, mabienthe, soluong, gia);
                                                    getthongtinorder gettt = new getthongtinorder()
                                                    {
                                                        order_id = lastInsertId,
                                                        product_id = masanpham,
                                                        variation_id = mabienthe,
                                                        order_quantity = soluong,
                                                        order_price = gia
                                                    };
                                                    lstgetthongtinorder.Add(gettt);
                                                    do
                                                    {
                                                        Console.Write("Do You Want To Continue?(Y/N): ");
                                                        cn = Console.ReadLine();
                                                        if (cn.ToLower() == "y")
                                                        {
                                                            chongia = 0;
                                                            chondc = 0;
                                                            chonsolg = 0;
                                                            chonbienthe = 0;
                                                            chonpay = 0;
                                                            chonsanpham = 0;
                                                            cn = "y";
                                                        }
                                                        else if (cn.ToLower() != "n" && cn.ToLower() != "y")
                                                        {
                                                            Console.WriteLine("Try Again");
                                                        }
                                                        else if (cn.ToLower() == "n")
                                                        {
                                                            float giaphaitra = 0;
                                                            foreach (getthongtinorder gtt in lstgetthongtinorder)
                                                            {
                                                                giaphaitra += gtt.order_price;
                                                            }
                                                            Console.WriteLine("You Need To Pay The Store The Amount Of: " + giaphaitra);
                                                            List<getpay> lstgetpay = tvpay.GetPayForCustomer(user);
                                                            Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            Console.WriteLine("| ID | Pay Name      | Bank Name     | Full Bank Name                                      |");
                                                            Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            foreach (getpay p in lstgetpay)
                                                            {
                                                                Console.WriteLine("| {0,-3}| {0,-14}| {1,-14}| {2,-52}|", p.pay_id, p.pay_name, p.pay_name_vt, p.pay_name_dd);
                                                                Console.WriteLine("+----+---------------+---------------+-----------------------------------------------------+");
                                                            }
                                                            while (chonpay != 0)
                                                            {
                                                                Console.Write("Please Enter Your Payment Method: ");
                                                                int thanhtoan = int.Parse(Console.ReadLine());
                                                                var ktmathanhtoan = lstgetpay.Find(x => x.pay_id == thanhtoan);
                                                                if (ktmathanhtoan == null)
                                                                {
                                                                    Console.WriteLine("Not Found, Please Try Again");
                                                                    chonpay = 1;
                                                                }
                                                                else
                                                                {
                                                                    List<delivery_address> lstaddressmoi = tvdeliveryaddress.GetAllDeliveryAddress(user);
                                                                    var getaddress = lstaddressmoi.Where(x => x.delivery_address_TrangThai == "on");
                                                                    if (getaddress.Count() == 0)
                                                                    {
                                                                        Console.WriteLine("List Your Delivery Address Is Empty");
                                                                        Console.WriteLine("Please Create A New Delivery Address");
                                                                        CUDDeliveryAddress themdiachi = new CUDDeliveryAddress();
                                                                        themdiachi.Add(user);
                                                                        kiemtradiachi = 1;
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        Console.WriteLine("| ID | Consignee Name     | Consignee Phonenumber | Consignee Address                                                       |");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        foreach (delivery_address ad in getaddress)
                                                                        {
                                                                            Console.WriteLine("| {0, -3}| {1, -19}| {2, -22}| {3, -72}|", ad.delivery_address_id, ad.consignee_name, ad.consignee_phonenumber, ad.fulladdress);
                                                                            Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        }
                                                                    }
                                                                    if (kiemtradiachi == 1)
                                                                    {
                                                                        tvdeliveryaddress = new TVDeliveryAddress();
                                                                        lstaddress = tvdeliveryaddress.GetAllDeliveryAddress(user);
                                                                        getaddress = lstaddress.Where(x => x.delivery_address_TrangThai == "on");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        Console.WriteLine("| ID | Consignee Name     | Consignee Phonenumber | Consignee Address                                                       |");
                                                                        Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        foreach (delivery_address ad in getaddress)
                                                                        {
                                                                            Console.WriteLine("| {0, -3}| {1, -19}| {2, -22}| {3, -72}|", ad.delivery_address_id, ad.consignee_name, ad.consignee_phonenumber, ad.fulladdress);
                                                                            Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                                                                        }
                                                                    }
                                                                    while (chondc != 0)
                                                                    {
                                                                        Console.Write("Enter The Delivery Address ID: ");
                                                                        madiachi = int.Parse(Console.ReadLine());
                                                                        var ktdc = lstaddress.Where(x => x.delivery_address_id == madiachi && x.delivery_address_TrangThai == "on");
                                                                        if (ktdc.Count() == 0)
                                                                        {
                                                                            Console.WriteLine("Not Found, Please Try Again");
                                                                            chondc = 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            var YN = "";
                                                                            do
                                                                            {
                                                                                Console.Write("Are You Sure?(Y/N):");
                                                                                YN = Console.ReadLine();
                                                                                if (YN.ToLower() == "y")
                                                                                {
                                                                                    foreach (getthongtinorder gtt in lstgetthongtinorder)
                                                                                    {
                                                                                        tvorderpay.AddOrderPay(gtt.order_id, thanhtoan, madiachi);
                                                                                    }
                                                                                    chongia = 0;
                                                                                    chondc = 0;
                                                                                    chonsolg = 0;
                                                                                    chonbienthe = 0;
                                                                                    chonpay = 0;
                                                                                    chonsanpham = 0;
                                                                                    cn = "n";
                                                                                }
                                                                                else if (YN.ToLower() != "n" && YN.ToLower() != "y")
                                                                                {
                                                                                    Console.WriteLine("Try Again");
                                                                                }
                                                                                else if (YN.ToLower() == "n")
                                                                                {
                                                                                    TVOrderPayAddress tvopa = new TVOrderPayAddress();
                                                                                    foreach (getthongtinorder g in lstgetthongtinorder)
                                                                                    {
                                                                                        tvopa.DeleteOrderPayAddress(g.order_id);
                                                                                    }
                                                                                    chongia = 0;
                                                                                    chondc = 0;
                                                                                    chonsolg = 0;
                                                                                    chonbienthe = 0;
                                                                                    chonpay = 0;
                                                                                    chonsanpham = 0;
                                                                                    cn = "n";
                                                                                }
                                                                            } while (YN.ToLower() != "y" && YN.ToLower() != "n");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    } while (cn.ToLower() != "n" && cn.ToLower() != "y");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } while (chongia != 0);
                } while (cn.ToLower() == "y");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void DeleteOrder(string user)
        {
            TVOrderPayAddress tvorderpayaddress = new TVOrderPayAddress();
            List<getorder> lstorder = tvorderpayaddress.GetAllOder();
            var lstorderforcustomer = lstorder.Where(x => x.account_username == user && x.order_TrangThai == "processing");
            if (lstorderforcustomer.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                Console.WriteLine("| ID |Product Name| Name     |Phonenumber| Consignee Address                                        |Color |Size|Price($)|Order Datetime        |Pay Name   |Quantity|Status     |");
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                foreach (getorder od in lstorderforcustomer)
                {
                    Console.WriteLine("| {0,-3}| {1,-11}| {2,-9}| {3,-10}|{4,-58}|{5,-6}|{6,-4}| {7,-7}|{8,-22}|{9,-11}| {10,-7}| {11, -10}|", od.order_id, od.product_name, od.consignee_name, od.consignee_phonenumber, od.consignee_address, od.color_name, od.size_name, od.order_price, od.order_datetime, od.pay_name, od.order_quantity, od.order_TrangThai);
                    Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                }
                int chonma = 0;
                do
                {
                    Console.Write("Enter The Order Id You Want To Delete: ");
                    int madonhang = int.Parse(Console.ReadLine());
                    var kiemtramadonhang = lstorderforcustomer.Where(x => x.order_id == madonhang);
                    if (kiemtramadonhang.Count() == 0)
                    {
                        Console.WriteLine("Not found");
                        do
                        {
                            Console.WriteLine("Do You Want To Exit?");
                            Console.WriteLine("0. Exit");
                            Console.WriteLine("1. Re-Enter");
                            Console.Write("--> ");
                            chonma = int.Parse(Console.ReadLine());
                            if (chonma != 0 && chonma != 1)
                            {
                                Console.WriteLine("Try Again");
                            }
                        } while (chonma != 0 && chonma != 1);
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
                            else if (yn.ToLower() == "n")
                            {
                                chonma = 0;
                            }
                            else if (yn.ToLower() == "y")
                            {
                                tvorderpayaddress.DeleteOrderPayAddress(madonhang);
                                chonma = 0;
                            }
                        } while (yn.ToLower() != "n" && yn.ToLower() != "y");
                    }
                } while (chonma != 0);
            }
        }
        public void DeleteOrderForAddmin()
        {
            TVOrderPayAddress tvorderpayaddress = new TVOrderPayAddress();
            List<getorder> lstorder = tvorderpayaddress.GetAllOder();
            var lstorderforadmin = lstorder.Where(x => x.order_TrangThai == "processing");
            if (lstorderforadmin.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                Console.WriteLine("| ID |Product Name| Name     |Phonenumber| Consignee Address                                        |Color |Size|Price($)|Order Datetime        |Pay Name   |Quantity|Status     |");
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                foreach (getorder od in lstorderforadmin)
                {
                    Console.WriteLine("| {0,-3}| {1,-11}| {2,-9}| {3,-10}|{4,-58}|{5,-6}|{6,-4}| {7,-7}|{8,-22}|{9,-11}| {10,-7}| {11, -10}|", od.order_id, od.product_name, od.consignee_name, od.consignee_phonenumber, od.consignee_address, od.color_name, od.size_name, od.order_price, od.order_datetime, od.pay_name, od.order_quantity, od.order_TrangThai);
                    Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                }
                int chonma = 0;
                do
                {
                    Console.Write("Enter The Order Id You Want To Delete: ");
                    int madonhang = int.Parse(Console.ReadLine());
                    var kiemtramadonhang = lstorderforadmin.Where(x => x.order_id == madonhang);
                    if (kiemtramadonhang.Count() == 0)
                    {
                        Console.WriteLine("Not found");
                        do
                        {
                            Console.WriteLine("Do You Want To Exit?");
                            Console.WriteLine("0. Exit");
                            Console.WriteLine("1. Re-Enter");
                            Console.Write("--> ");
                            chonma = int.Parse(Console.ReadLine());
                            if (chonma != 0 && chonma != 1)
                            {
                                Console.WriteLine("Try Again");
                            }
                        } while (chonma != 0 && chonma != 1);
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
                            else if (yn.ToLower() == "n")
                            {
                                chonma = 0;
                            }
                            else if (yn.ToLower() == "y")
                            {
                                tvorderpayaddress.DeleteOrderPayAddress(madonhang);
                                chonma = 0;
                            }
                        } while (yn.ToLower() != "n" && yn.ToLower() != "y");
                    }
                } while (chonma != 0);
            }
        }
    }
}