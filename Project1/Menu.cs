using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1
{
    public class Menu
    {
        public void FashionShop()
        {
            Console.WriteLine(@"
███████  █████  ███████ ██   ██ ██  ██████  ███    ██     ███████ ██   ██  ██████  ██████
██      ██   ██ ██      ██   ██ ██ ██    ██ ████   ██     ██      ██   ██ ██    ██ ██   ██
█████   ███████ ███████ ███████ ██ ██    ██ ██ ██  ██     ███████ ███████ ██    ██ ██████
██      ██   ██      ██ ██   ██ ██ ██    ██ ██  ██ ██          ██ ██   ██ ██    ██ ██
██      ██   ██ ███████ ██   ██ ██  ██████  ██   ████     ███████ ██   ██  ██████  ██");
        }
        public void MenuChinh()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Log In                         |");
            Console.WriteLine("| 2. Register                       |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuAdmin()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|         ADMIN FUNCTIONS           |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Log Out                        |");
            Console.WriteLine("| 1. Customers Management           |");
            Console.WriteLine("| 2. Products Management            |");
            Console.WriteLine("| 3. Orders Management              |");
            Console.WriteLine("| 4. Categories Management          |");
            Console.WriteLine("| 5. Color Management               |");
            Console.WriteLine("| 6. Size Management                |");
            Console.WriteLine("| 7. Pay Management                 |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuQuanLiKhachHang()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|       CUSTOMERS MANAGEMENT        |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show List Of Customers         |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuQuanLiSanPham()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|        PRODUCTS MANAGEMENT        |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show List Of Products          |");
            Console.WriteLine("| 2. Search Product                 |");
            Console.WriteLine("| 3. Add New Product                |");
            Console.WriteLine("| 4. Add New Variation              |");
            Console.WriteLine("| 5. Update Product                 |");
            Console.WriteLine("| 6. Update Variation               |");
            Console.WriteLine("| 7. Delete Product                 |");
            Console.WriteLine("| 8. Delete Variation               |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuTimKiemSanPham()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|          SEARCH PRODUCT           |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Search Active Product          |");
            Console.WriteLine("| 2. Search Not Active Product      |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuTimKiemTatCaSanPham()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|       SEARCH ACTIVE PRODUCT       |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Search By Category Name        |");
            Console.WriteLine("| 2. Search By Product Name         |");
            Console.WriteLine("| 3. Search By Price                |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuTimSanPhamHoatDong()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|     SEARCH NOT ACTIVE PRODUCT     |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Search By Category Name        |");
            Console.WriteLine("| 2. Search By Product Name         |");
            Console.WriteLine("| 3. Search By Price                |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuQuanLiDonHang()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|         ORDER MANAGEMENT          |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show List Of Orders            |");
            Console.WriteLine("| 2. Accept New Orders              |");
            Console.WriteLine("| 3. Cancellation Orders            |");
            Console.WriteLine("| 4. Revenue For Month              |");
            Console.WriteLine("| 5. The Product Sold In The Month  |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuQuanLiDanhMuc()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|       CATEGORIES MANAGEMENT       |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show List Of Categories        |");
            Console.WriteLine("| 2. Add New Category               |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuQuanLiMau()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|         COLORS MANAGEMENT         |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show List Of Colors            |");
            Console.WriteLine("| 2. Add New Color                  |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuQuanLiSize()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|         SIZE MANAGEMENT           |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show List Of Sizes             |");
            Console.WriteLine("| 2. Add New Size                   |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void Menuquanlithanhtoan()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|        PAYMENT MANAGEMENT         |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show List Of Payment           |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuKhachHang()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Log Out                        |");
            Console.WriteLine("| 1. Delivery Address               |");
            Console.WriteLine("| 2. Order                          |");
            Console.WriteLine("| 3. Account Management             |");
            Console.WriteLine("| 4. Pay Management                 |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuDiaChiNhanHang()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|          DELIVERY ADDRESS         |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show List Of Delivery Address  |");
            Console.WriteLine("| 2. Add New Delivery Address       |");
            Console.WriteLine("| 3. Update Delivery Address        |");
            Console.WriteLine("| 4. Delete Delivery Address        |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuDatHang()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|               ORDERS              |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Search Product To Order        |");
            Console.WriteLine("| 2. Show Your Orders Placed        |");
            Console.WriteLine("| 3. Order cancellation             |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuTimSanPhanDatHang()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|     SEARCH PRODUCT TO ORDER       |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show List Of Products          |");
            Console.WriteLine("| 2. Search By Category Name        |");
            Console.WriteLine("| 3. Search By Product Name         |");
            Console.WriteLine("| 4. Search By Price                |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuQuanLiTaiKhoan()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|        ACCOUNT MANAGEMENT         |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Account Information            |");
            Console.WriteLine("| 2. Update Your Profile            |");
            Console.WriteLine("| 3. Delete Your Account            |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuSuaTaiKhoan()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|          UPDATE PROFILE           |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Update Your Name               |");
            Console.WriteLine("| 2. Update Your Phone Number       |");
            Console.WriteLine("| 3. Update Your Email              |");
            Console.WriteLine("| 4. Update Your Password           |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
        public void MenuQuanLiThanhToan()
        {
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("|          PAY MANAGEMENT           |");
            Console.WriteLine("+-----------------------------------+");
            Console.WriteLine("| 0. Exit                           |");
            Console.WriteLine("| 1. Show Your Payment              |");
            Console.WriteLine("| 2. Create Your Payment            |");
            Console.WriteLine("+-----------------------------------+");
            Console.Write("--> ");
        }
    }
}