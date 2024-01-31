using System.Drawing;
using System.IO.Enumeration;
using System.Linq.Expressions;
using System.IO.Compression;
using Microsoft.VisualBasic.CompilerServices;
using System.ComponentModel;
using BL;
using DAL.DBContext;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Collections.Generic;
using System;
using DAL.Services;
using BL.View;
using Project1;
using System.Text.RegularExpressions;
using BL.Controller;

DiaChi dcmmm = new DiaChi();
Console.OutputEncoding = Encoding.UTF8;
Menu mn = new Menu();
int chon;
string user;
do
{
    mn.FashionShop();
    mn.MenuChinh();
    chon = int.Parse(Console.ReadLine());
    switch (chon)
    {
        case 0:
            {
                Console.WriteLine("You Did Exit");
                break;
            }
        case 1://Dang nhap
            {
                Console.WriteLine("-------");
                Console.WriteLine("|Login|");
                Console.WriteLine("-------");
                string column;
                string table;
                string value;
                int up = 0;
                do
                {
                    CUDHidePassword ck = new CUDHidePassword();
                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    user = username;
                    Console.Write("Password: ");
                    string password = ck.GetPassword();
                    TVLogIn lg = new TVLogIn();
                    bool kiemtra = lg.Log(username, password);
                    if (!kiemtra)
                    {
                        Console.WriteLine("The Username Or Password You Entered Is Wrong, Please Re-Enter Or Create A New Account");
                        do
                        {
                            Console.WriteLine("Do You Want To Exit?");
                            Console.WriteLine("1. Try Again");
                            Console.WriteLine("0. Exit");
                            Console.Write("Your Choice: ");
                            up = int.Parse(Console.ReadLine());
                            if (up != 0 && up != 1)
                            {
                                Console.WriteLine("Choice Again");
                            }
                        } while (up != 0 && up != 1);
                        if (up == 0)
                        {
                            Console.WriteLine("Exited");
                        }
                    }
                    else
                    {
                        TVAccount check = new TVAccount();
                        bool accountCheck = check.accountCheck(username, password);
                        if (accountCheck)//ADmin
                        {
                            int chonKH;
                            do
                            {
                                mn.MenuAdmin();
                                chonKH = int.Parse(Console.ReadLine());
                                switch (chonKH)//chức năng của admin
                                {
                                    case 0://Đăng xuất
                                        {
                                            Console.WriteLine("You Did Log Out");
                                            up = 0;
                                            break;
                                        }
                                    case 1:// quản lí khách hàng
                                        {

                                            int chon1;
                                            do
                                            {
                                                mn.MenuQuanLiKhachHang();
                                                chon1 = int.Parse(Console.ReadLine());
                                                switch (chon1)
                                                {
                                                    case 0:
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1://Hiển thị danh sách khách hàng
                                                        {
                                                            Console.WriteLine("----------------------");
                                                            Console.WriteLine("| Show All Customers |");
                                                            Console.WriteLine("----------------------");
                                                            TaiKhoan tk = new TaiKhoan();
                                                            tk.GetAll();
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();

                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (chon1 != 0);
                                            break;
                                        }
                                    case 2://Quản lý sản phẩm
                                        {
                                            int chon2;
                                            do
                                            {
                                                mn.MenuQuanLiSanPham();
                                                chon2 = int.Parse(Console.ReadLine());
                                                switch (chon2)
                                                {
                                                    case 0:
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1://Hiển thị danh sách sản phẩm
                                                        {
                                                            Console.WriteLine("---------------------");
                                                            Console.WriteLine("| Show All Products |");
                                                            Console.WriteLine("---------------------");
                                                            SanPham htallsp = new SanPham();
                                                            htallsp.HienThiAllSP();
                                                            break;
                                                        }
                                                    case 2://Tìm kiếm 
                                                        {
                                                            int TSP;
                                                            do
                                                            {
                                                                mn.MenuTimKiemSanPham();
                                                                TSP = int.Parse(Console.ReadLine());
                                                                switch (TSP)
                                                                {
                                                                    case 0:
                                                                        {
                                                                            Console.WriteLine("Exited");
                                                                            break;
                                                                        }
                                                                    case 1:// tìm sản phẩm hoạt động
                                                                        {
                                                                            int Tsp;
                                                                            do
                                                                            {
                                                                                mn.MenuTimKiemTatCaSanPham();
                                                                                Tsp = int.Parse(Console.ReadLine());
                                                                                switch (Tsp)
                                                                                {
                                                                                    case 0:
                                                                                        {
                                                                                            Console.WriteLine("Exited");
                                                                                            break;
                                                                                        }
                                                                                    case 1:// tìm theo tên danh mục
                                                                                        {
                                                                                            Console.WriteLine("----------------------");
                                                                                            Console.WriteLine("| Search By Category |");
                                                                                            Console.WriteLine("----------------------");
                                                                                            string yn = "";
                                                                                            do
                                                                                            {
                                                                                                Console.Write("Enter Your Search: ");
                                                                                                value = Console.ReadLine();
                                                                                                string product_onoff = "on";
                                                                                                string variation_onoff = "on";
                                                                                                column = "category_name";
                                                                                                SanPham htsp = new SanPham();
                                                                                                htsp.SearchByCategoryName(value, product_onoff, variation_onoff);
                                                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                yn = Console.ReadLine().ToLower();
                                                                                                while (yn != "n" && yn != "y")
                                                                                                {
                                                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                    yn = Console.ReadLine().ToLower();
                                                                                                }
                                                                                            } while (yn == "y");
                                                                                            break;
                                                                                        }
                                                                                    case 2:// tìm theo tên sản phẩm
                                                                                        {
                                                                                            string yn = "";
                                                                                            do
                                                                                            {
                                                                                                Console.Write("Enter Your Search: ");
                                                                                                value = Console.ReadLine();
                                                                                                string product_onoff = "on";
                                                                                                string variation_onoff = "on";
                                                                                                column = "product_name";
                                                                                                SanPham htsp = new SanPham();
                                                                                                htsp.SearchByProductName(value, product_onoff, variation_onoff);
                                                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                yn = Console.ReadLine().ToLower();
                                                                                                while (yn != "n" && yn != "y")
                                                                                                {
                                                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                    yn = Console.ReadLine().ToLower();
                                                                                                }
                                                                                            } while (yn == "y");
                                                                                            break;
                                                                                        }
                                                                                    case 3:// tìm theo giá
                                                                                        {
                                                                                            Console.WriteLine("-------------------");
                                                                                            Console.WriteLine("| Search By Price |");
                                                                                            Console.WriteLine("-------------------");
                                                                                            string yn = "";
                                                                                            do
                                                                                            {
                                                                                                float valuemin, valuemax;
                                                                                                string product_onoff = "on";
                                                                                                string variation_onoff = "on";
                                                                                                do
                                                                                                {
                                                                                                    Console.Write("Enter Minimum Price: ");
                                                                                                    valuemin = float.Parse(Console.ReadLine());
                                                                                                    Console.Write("Enter Maximum Price: ");
                                                                                                    valuemax = float.Parse(Console.ReadLine());
                                                                                                    if (valuemax < valuemin)
                                                                                                    {
                                                                                                        Console.WriteLine("The Maximum Price Must Be Greater Than The Minimum Price Please Try Again");
                                                                                                    }
                                                                                                } while (valuemax < valuemin);
                                                                                                SanPham htsp = new SanPham();
                                                                                                htsp.SearchByPrice(valuemax, valuemin, product_onoff, variation_onoff);
                                                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                yn = Console.ReadLine().ToLower();
                                                                                                while (yn != "n" && yn != "y")
                                                                                                {
                                                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                    yn = Console.ReadLine().ToLower();
                                                                                                }
                                                                                            } while (yn == "y");
                                                                                            break;
                                                                                        }
                                                                                    default:
                                                                                        {
                                                                                            Console.WriteLine("Try Again");
                                                                                            break;
                                                                                        }
                                                                                }
                                                                            } while (Tsp != 0);
                                                                            break;
                                                                        }
                                                                    case 2:// tìm sản phẩm không hoạt động
                                                                        {
                                                                            int Tsphd;
                                                                            do
                                                                            {
                                                                                mn.MenuTimSanPhamHoatDong();
                                                                                Tsphd = int.Parse(Console.ReadLine());
                                                                                switch (Tsphd)
                                                                                {
                                                                                    case 0:
                                                                                        {
                                                                                            Console.WriteLine("Exited");
                                                                                            break;
                                                                                        }
                                                                                    case 1:// tìm theo tên danh mục
                                                                                        {
                                                                                            Console.WriteLine("----------------------");
                                                                                            Console.WriteLine("| Search By Category |");
                                                                                            Console.WriteLine("----------------------");
                                                                                            string yn = "";
                                                                                            do
                                                                                            {
                                                                                                Console.Write("Enter Your Search: ");
                                                                                                value = Console.ReadLine();
                                                                                                string product_onoff = "off";
                                                                                                string variation_onoff = "off";
                                                                                                column = "category_name";
                                                                                                SanPham htsp = new SanPham();
                                                                                                htsp.SearchByCategoryName(value, product_onoff, variation_onoff);
                                                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                yn = Console.ReadLine().ToLower();
                                                                                                while (yn != "n" && yn != "y")
                                                                                                {
                                                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                    yn = Console.ReadLine().ToLower();
                                                                                                }
                                                                                            } while (yn == "y");
                                                                                            break;
                                                                                        }
                                                                                    case 2:// tìm theo tên sản phẩm
                                                                                        {
                                                                                            Console.WriteLine("---------------------");
                                                                                            Console.WriteLine("| Search By Product |");
                                                                                            Console.WriteLine("---------------------");
                                                                                            string yn = "";
                                                                                            do
                                                                                            {
                                                                                                Console.Write("Enter Your Search: ");
                                                                                                value = Console.ReadLine();
                                                                                                string product_onoff = "off";
                                                                                                string variation_onoff = "off";
                                                                                                column = "product_name";
                                                                                                SanPham htsp = new SanPham();
                                                                                                htsp.SearchByProductName(value, product_onoff, variation_onoff);
                                                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                yn = Console.ReadLine().ToLower();
                                                                                                while (yn != "n" && yn != "y")
                                                                                                {
                                                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                    yn = Console.ReadLine().ToLower();
                                                                                                }
                                                                                            } while (yn == "y");
                                                                                            break;
                                                                                        }
                                                                                    case 3:// tìm theo giá
                                                                                        {

                                                                                            Console.WriteLine("-------------------");
                                                                                            Console.WriteLine("| Search By Price |");
                                                                                            Console.WriteLine("-------------------");
                                                                                            string yn = "";
                                                                                            do
                                                                                            {
                                                                                                float valuemin, valuemax;
                                                                                                string product_onoff = "off";
                                                                                                string variation_onoff = "off";
                                                                                                do
                                                                                                {
                                                                                                    Console.Write("Enter Minimum Price: ");
                                                                                                    valuemin = float.Parse(Console.ReadLine());
                                                                                                    Console.Write("Enter Maximum Price: ");
                                                                                                    valuemax = float.Parse(Console.ReadLine());
                                                                                                    if (valuemax < valuemin)
                                                                                                    {
                                                                                                        Console.WriteLine("The Maximum Price Must Be Greater Than The Minimum Price Please Try Again");
                                                                                                    }
                                                                                                } while (valuemax < valuemin);
                                                                                                SanPham htsp = new SanPham();
                                                                                                htsp.SearchByPrice(valuemax, valuemin, product_onoff, variation_onoff);
                                                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                yn = Console.ReadLine().ToLower();
                                                                                                while (yn != "n" && yn != "y")
                                                                                                {
                                                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                                                    yn = Console.ReadLine().ToLower();
                                                                                                }
                                                                                            } while (yn == "y");
                                                                                            break;
                                                                                        }
                                                                                    default:
                                                                                        {
                                                                                            Console.WriteLine("Try Again");
                                                                                            break;
                                                                                        }
                                                                                }
                                                                            } while (Tsphd != 0);
                                                                            break;
                                                                        }
                                                                }
                                                            } while (TSP != 0);
                                                            break;
                                                        }
                                                    case 3:// Thêm sản phẩm
                                                        {
                                                            Console.WriteLine("-------------------");
                                                            Console.WriteLine("| Add New Product |");
                                                            Console.WriteLine("-------------------");
                                                            string yn = "";
                                                            do
                                                            {
                                                                CUDProductVariation cud = new CUDProductVariation();
                                                                cud.Add();
                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                yn = Console.ReadLine().ToLower();
                                                                while (yn != "n" && yn != "y")
                                                                {
                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                    yn = Console.ReadLine().ToLower();
                                                                }
                                                            } while (yn == "y");
                                                            break;
                                                        }
                                                    case 4: // them bien the
                                                        {
                                                            Console.WriteLine("---------------------");
                                                            Console.WriteLine("| Add New Variation |");
                                                            Console.WriteLine("---------------------");
                                                            string yn = "";
                                                            do
                                                            {
                                                                CUDVariation cudbt = new CUDVariation();
                                                                cudbt.AddVariation();
                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                yn = Console.ReadLine().ToLower();
                                                                while (yn != "n" && yn != "y")
                                                                {
                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                    yn = Console.ReadLine().ToLower();
                                                                }
                                                            } while (yn == "y");
                                                            break;
                                                        }
                                                    case 5: // sửa sản phẩm
                                                        {

                                                            Console.WriteLine("------------------");
                                                            Console.WriteLine("| Update Product |");
                                                            Console.WriteLine("------------------");
                                                            string yn = "";
                                                            do
                                                            {
                                                                CUDProduct uproduct = new CUDProduct();
                                                                uproduct.UpdateProduct();
                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                yn = Console.ReadLine().ToLower();
                                                                while (yn != "n" && yn != "y")
                                                                {
                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                    yn = Console.ReadLine().ToLower();
                                                                }
                                                            } while (yn == "y");
                                                            break;
                                                        }
                                                    case 6:// sửa biến thể
                                                        {
                                                            Console.WriteLine("--------------------");
                                                            Console.WriteLine("| Update Variation |");
                                                            Console.WriteLine("--------------------");
                                                            string yn = "";
                                                            do
                                                            {
                                                                CUDVariation uvariation = new CUDVariation();
                                                                uvariation.UpdateVariation();
                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                yn = Console.ReadLine().ToLower();
                                                                while (yn != "n" && yn != "y")
                                                                {
                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                    yn = Console.ReadLine().ToLower();
                                                                }
                                                            } while (yn == "y");
                                                            break;
                                                        }
                                                    case 7:// xóa sản phẩm
                                                        {
                                                            Console.WriteLine("------------------");
                                                            Console.WriteLine("| Delete Product |");
                                                            Console.WriteLine("------------------");
                                                            string yn = "";
                                                            do
                                                            {
                                                                CUDProduct dlt = new CUDProduct();
                                                                dlt.DeleteProduct();
                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                yn = Console.ReadLine().ToLower();
                                                                while (yn != "n" && yn != "y")
                                                                {
                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                    yn = Console.ReadLine().ToLower();
                                                                }
                                                            } while (yn == "y");
                                                            break;
                                                        }
                                                    case 8: //xoa bien the
                                                        {
                                                            Console.WriteLine("--------------------");
                                                            Console.WriteLine("| Delete Variation |");
                                                            Console.WriteLine("--------------------");
                                                            string yn = "";
                                                            do
                                                            {
                                                                CUDVariation dlt = new CUDVariation();
                                                                dlt.DeleteVariation();
                                                                Console.Write("Do You Want To Continue? (Y/N): ");
                                                                yn = Console.ReadLine().ToLower();
                                                                while (yn != "n" && yn != "y")
                                                                {
                                                                    Console.WriteLine("Invalid input. Please try again.");
                                                                    Console.Write("Do You Want To Continue? (Y/N): ");
                                                                    yn = Console.ReadLine().ToLower();
                                                                }
                                                            } while (yn == "y");
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (chon2 != 0);
                                            break;
                                        }
                                    case 3://quản lí đơn hàng
                                        {
                                            int chon3;
                                            do
                                            {
                                                mn.MenuQuanLiDonHang();
                                                chon3 = int.Parse(Console.ReadLine());
                                                switch (chon3)
                                                {
                                                    case 0://quay lại
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1://Hiển thị tất cả đơn hàng
                                                        {
                                                            Console.WriteLine("-------------------");
                                                            Console.WriteLine("| Show All Orders |");
                                                            Console.WriteLine("-------------------");
                                                            DonHang Dh = new DonHang();
                                                            Dh.GetAllOder();
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    case 2: // check don hang moi
                                                        {
                                                            Console.WriteLine("---------------------");
                                                            Console.WriteLine("| Accept New Orders |");
                                                            Console.WriteLine("---------------------");
                                                            CUDOrderPay cud = new CUDOrderPay();
                                                            cud.CheckNewOrder();
                                                            break;
                                                        }
                                                    case 3:// huy don hang
                                                        {
                                                            Console.WriteLine("---------------------");
                                                            Console.WriteLine("| Delete New Orders |");
                                                            Console.WriteLine("---------------------");
                                                            CUDOrder deleteorder = new CUDOrder();
                                                            deleteorder.DeleteOrderForAddmin();
                                                            break;
                                                        }
                                                    case 4: // tong doanh thu trong 1 thang
                                                        {
                                                            CUDOrderPay tongdoanhthu = new CUDOrderPay();
                                                            tongdoanhthu.RevenueThisMonth();
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    case 5:// san pham da ban trong thang
                                                        {
                                                            CUDOrderPay cud = new CUDOrderPay();
                                                            cud.ProductSold();
                                                            Console.Write("\nEnter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (chon3 != 0);
                                            break;
                                        }
                                    case 4:// quản lý danh mục
                                        {
                                            int chonQLDM;
                                            do
                                            {
                                                mn.MenuQuanLiDanhMuc();
                                                chonQLDM = int.Parse(Console.ReadLine());
                                                switch (chonQLDM)
                                                {
                                                    case 0:
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1://show toàn bộ danh mục
                                                        {
                                                            DanhMuc dm = new DanhMuc();
                                                            dm.GetAll();
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    case 2:// thêm danh mục
                                                        {
                                                            CUDCategory dm = new CUDCategory();
                                                            dm.Add();
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (chonQLDM != 0);
                                            break;
                                        }
                                    case 5: // quan li mau
                                        {
                                            int chonQLMau;
                                            do
                                            {
                                                mn.MenuQuanLiMau();
                                                chonQLMau = int.Parse(Console.ReadLine());
                                                switch (chonQLMau)
                                                {
                                                    case 0:
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1://show toàn bộ mau
                                                        {
                                                            Mau cl = new Mau();
                                                            cl.GetAll();
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    case 2:// thêm mau
                                                        {
                                                            CUDColor tcl = new CUDColor();
                                                            tcl.Add();
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (chonQLMau != 0);
                                            break;
                                        }
                                    case 6:// quan li size
                                        {
                                            int chonQLSize;
                                            do
                                            {
                                                mn.MenuQuanLiSize();
                                                chonQLSize = int.Parse(Console.ReadLine());
                                                switch (chonQLSize)
                                                {
                                                    case 0:
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1://show toàn bộ size
                                                        {
                                                            KichThuoc sz = new KichThuoc();
                                                            sz.GetAll();
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    case 2:// thêm size
                                                        {
                                                            CUDSize tsz = new CUDSize();
                                                            tsz.Add();
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (chonQLSize != 0);
                                            break;
                                        }
                                    case 7: // hien thi phuong thuc thanh toan
                                        {
                                            int chontt;
                                            do
                                            {
                                                mn.Menuquanlithanhtoan();
                                                chontt = int.Parse(Console.ReadLine());
                                                switch (chontt)
                                                {
                                                    case 0:
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1://show toàn bộ pay
                                                        {
                                                            ThanhToan thanhtoan = new ThanhToan();
                                                            thanhtoan.GetAll();
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (chontt != 0);
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Try Again");
                                            break;
                                        }
                                }
                            } while (chonKH != 0);
                        }
                        else//Khach hang
                        {
                            int chonAD;
                            do
                            {
                                mn.MenuKhachHang();
                                chonAD = int.Parse(Console.ReadLine());
                                switch (chonAD)//Cac chuc nang cua khach hang
                                {
                                    case 0://Đăng xuất
                                        {
                                            Console.WriteLine("You Did Log Out");
                                            break;
                                        }
                                    case 1://địa chỉ nhận hàng
                                        {
                                            int dc;
                                            do
                                            {
                                                mn.MenuDiaChiNhanHang();
                                                dc = int.Parse(Console.ReadLine());
                                                switch (dc)
                                                {
                                                    case 0:
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1:// show dia chi
                                                        {
                                                            string accounnt_username = user;
                                                            DiaChi diachi = new DiaChi();
                                                            diachi.GetAll(accounnt_username);
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    case 2:// them dia chi
                                                        {
                                                            string account_username = user;
                                                            CUDDeliveryAddress tdc = new CUDDeliveryAddress();
                                                            tdc.Add(account_username);
                                                            break;
                                                        }
                                                    case 3: // sua dia chi
                                                        {
                                                            CUDDeliveryAddress sua = new CUDDeliveryAddress();
                                                            sua.Update(user);
                                                            break;
                                                        }
                                                    case 4: // xoa dia chi
                                                        {
                                                            CUDDeliveryAddress xoa = new CUDDeliveryAddress();
                                                            xoa.Delete(user);
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (dc != 0);
                                            break;
                                        }
                                    case 2://đặt hàng
                                        {
                                            int chon4;
                                            do
                                            {
                                                mn.MenuDatHang();
                                                chon4 = int.Parse(Console.ReadLine());
                                                switch (chon4)
                                                {
                                                    case 0://Quay lại
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1:// tìm kiếm sản phẩm
                                                        {
                                                            int mh;
                                                            do
                                                            {
                                                                mn.MenuTimSanPhanDatHang();
                                                                mh = int.Parse(Console.ReadLine());
                                                                switch (mh)
                                                                {
                                                                    case 0:
                                                                        {
                                                                            Console.WriteLine("Exited");
                                                                            break;
                                                                        }
                                                                    case 1: // show all de dat hang
                                                                        {
                                                                            CUDOrder add = new CUDOrder();
                                                                            add.GetByShowProduct(user);
                                                                            break;
                                                                        }
                                                                    case 2: // tim theo ten danh muc
                                                                        {
                                                                            CUDOrder add = new CUDOrder();
                                                                            add.GetByCategory(user);
                                                                            break;
                                                                        }
                                                                    case 3: // tim theo ten san pham
                                                                        {
                                                                            CUDOrder add = new CUDOrder();
                                                                            add.GetByProduct(user);
                                                                            break;
                                                                        }
                                                                    case 4: // tim theo gia
                                                                        {
                                                                            CUDOrder add = new CUDOrder();
                                                                            add.GetByPrice(user);
                                                                            break;
                                                                        }
                                                                    default:
                                                                        {
                                                                            Console.WriteLine("Try Again");
                                                                            break;
                                                                        }
                                                                }
                                                            } while (mh != 0);
                                                            break;
                                                        }
                                                    case 2://xem đơn hàng đã đặt
                                                        {
                                                            DonHang xdh = new DonHang();
                                                            xdh.GetOrderForCustomer(user);
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    case 3:// hủy đơn hàng
                                                        {
                                                            CUDOrder deleteorder = new CUDOrder();
                                                            deleteorder.DeleteOrder(user);
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (chon4 != 0);
                                            break;
                                        }
                                    case 3:// quản lí tài khoản
                                        {
                                            int TK;
                                            do
                                            {
                                                mn.MenuQuanLiTaiKhoan();
                                                TK = int.Parse(Console.ReadLine());
                                                switch (TK)
                                                {
                                                    case 0:  // Thoát
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1:// thông tin tài khoản
                                                        {
                                                            value = user;
                                                            TaiKhoan tk = new TaiKhoan();
                                                            tk.GetTK(value);
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    case 2:// sửa thông tin tài khoản
                                                        {
                                                            int sua;
                                                            do
                                                            {
                                                                mn.MenuSuaTaiKhoan();
                                                                sua = int.Parse(Console.ReadLine());
                                                                switch (sua)
                                                                {
                                                                    case 1://sửa tên
                                                                        {
                                                                            TVAccount tva = new TVAccount();
                                                                            int c;
                                                                            do
                                                                            {
                                                                                Console.Write("Your Name: ");
                                                                                value = Console.ReadLine();
                                                                                if (Regex.IsMatch(value, "^[a-z ]+$"))
                                                                                {
                                                                                    c = 0;
                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.WriteLine("Invalid Name, Please Try Again");
                                                                                    c = 1;
                                                                                }
                                                                            } while (c != 0);
                                                                            string account_username = user;
                                                                            tva.UpdateAccountInfoName(account_username, value);
                                                                            Console.WriteLine("Successful");
                                                                            Console.Write("Enter Any Key To Exit...");
                                                                            Console.ReadKey();
                                                                            Console.Write("\n");
                                                                            break;
                                                                        }
                                                                    case 2://sửa số điện thoại
                                                                        {
                                                                            string account_username = user;
                                                                            CUDAccount sacc = new CUDAccount();
                                                                            sacc.UpdatePhoneNumber(account_username);
                                                                            Console.Write("Enter Any Key To Exit...");
                                                                            Console.ReadKey();
                                                                            Console.Write("\n");
                                                                            break;
                                                                        }
                                                                    case 3://sửa email
                                                                        {
                                                                            string account_username = user;
                                                                            CUDAccount sacc = new CUDAccount();
                                                                            sacc.UpdateEmail(account_username);
                                                                            Console.Write("Enter Any Key To Exit...");
                                                                            Console.ReadKey();
                                                                            Console.Write("\n");
                                                                            break;
                                                                        }
                                                                    case 4:// sửa mật khẩu
                                                                        {
                                                                            CUDAccount updatepass = new CUDAccount();
                                                                            updatepass.UpdatePassword(user);
                                                                            Console.WriteLine("Successfull");
                                                                            break;
                                                                        }
                                                                    case 0:
                                                                        {
                                                                            Console.WriteLine("Exited");
                                                                            break;
                                                                        }
                                                                    default:
                                                                        {
                                                                            Console.WriteLine("Try Again");
                                                                            break;
                                                                        }
                                                                }
                                                            } while (sua != 0);
                                                            break;
                                                        }
                                                    case 3: // xóa tài khoản
                                                        {
                                                            CUDAccount deleteacc = new CUDAccount();
                                                            deleteacc.DeleteAccount(user);
                                                            TK = 0;
                                                            chonAD = 0;
                                                            up = 0;
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (TK != 0);
                                            break;
                                        }
                                    case 4: // quan li thanh toan
                                        {
                                            int chonqltt = 0;
                                            do
                                            {
                                                mn.MenuQuanLiThanhToan();
                                                chonqltt = int.Parse(Console.ReadLine());
                                                switch (chonqltt)
                                                {
                                                    case 0:
                                                        {
                                                            Console.WriteLine("Exited");
                                                            break;
                                                        }
                                                    case 1: // hien thi thanh toan cua minh
                                                        {
                                                            ThanhToan tht = new ThanhToan();
                                                            tht.GetPayment(user);
                                                            Console.Write("Enter Any Key To Exit...");
                                                            Console.ReadKey();
                                                            Console.Write("\n");
                                                            break;
                                                        }
                                                    case 2: // tao thanh toan
                                                        {
                                                            CUDPay cudpay = new CUDPay();
                                                            cudpay.AddPay(user);
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("Try Again");
                                                            break;
                                                        }
                                                }
                                            } while (chonqltt != 0);
                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Try Again");
                                            break;
                                        }
                                }
                            } while (chonAD != 0);
                        }
                    }
                } while (up != 0);
                break;
            }
        case 2://Dang ki
            {
                Console.WriteLine("----------");
                Console.WriteLine("|Register|");
                Console.WriteLine("----------");
                Register dk = new Register();
                dk.DangKy();
                break;
            }
        default:
            {
                Console.WriteLine("Try Again");
                break;
            }
    }
} while (chon != 0);