using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.View
{
    public class DiaChi
    {
        public void GetAll(string accounnt_username)
        {
            Console.WriteLine("YOUR DELIVERY ADDRESS");
            TVDeliveryAddress tv = new TVDeliveryAddress();
            List<delivery_address> deliveryList = tv.GetAllDeliveryAddress(accounnt_username);
            var get = deliveryList.Where(x => x.delivery_address_TrangThai == "on");
            if (get.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                Console.WriteLine("| ID | Consignee Name     | Consignee Phonenumber | Consignee Address                                                       |");
                Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                foreach (delivery_address d in get)
                {
                    Console.WriteLine("| {0, -3}| {1, -19}| {2, -22}| {3, -72}|", d.delivery_address_id, d.consignee_name, d.consignee_phonenumber, d.fulladdress);
                    Console.WriteLine("+----+--------------------+-----------------------+-------------------------------------------------------------------------+");
                }
            }
        }
        public string GetTinh()
        {
            string input = "";
            TVDeliveryAddress tvdeliveryaddress = new TVDeliveryAddress();
            int pageNumber = 1;
            int pageSize = 10;
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = startIndex + pageSize - 1;
            bool exit = false;
            List<tinhthanhpho> lsttinhthanhpho = tvdeliveryaddress.GetTinh(pageSize, startIndex);
            List<tinhthanhpho> demtinhthanhpho = tvdeliveryaddress.GetTinh(100, 0);
            int count = demtinhthanhpho.Count();
            int totalProducts = count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            Console.WriteLine("+-----+------------------------------+");
            Console.WriteLine("| ID  | City                         |");
            Console.WriteLine("+-----+------------------------------+");
            foreach (tinhthanhpho ttp in lsttinhthanhpho)
            {
                Console.WriteLine("| {0, -4}| {1, -29}|", ttp.matp, ttp.name);
                Console.WriteLine("+-----+------------------------------+");
            }
            Console.WriteLine("                             ({0}/{1})", pageNumber, totalPages);
            while (!exit)
            {
                Console.WriteLine("Enter 'N' To Next Page");
                Console.WriteLine("Enter 'P' To Back Page");
                Console.WriteLine("Enter 'Q' To Exit");
                Console.WriteLine("Enter Number Is City Id");
                Console.Write("--> ");
                input = Console.ReadLine();
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
                        List<tinhthanhpho> getttp = tvdeliveryaddress.GetTinh(pageSize, startIndex);
                        Console.WriteLine("+-----+------------------------------+");
                        Console.WriteLine("| ID  | City                         |");
                        Console.WriteLine("+-----+------------------------------+");
                        foreach (tinhthanhpho ttp in getttp)
                        {
                            Console.WriteLine("| {0, -4}| {1, -29}|", ttp.matp, ttp.name);
                            Console.WriteLine("+-----+------------------------------+");
                        }
                        Console.WriteLine("                             ({0}/{1})", pageNumber, totalPages);
                    }
                }
                else if (input.ToLower() == "p")
                {
                    if (pageNumber > 1)
                    {
                        pageNumber--;
                        startIndex = (pageNumber - 1) * pageSize;
                        endIndex = startIndex + pageSize - 1;
                        List<tinhthanhpho> getttpt = tvdeliveryaddress.GetTinh(pageSize, startIndex);
                        Console.WriteLine("+-----+------------------------------+");
                        Console.WriteLine("| ID  | City                         |");
                        Console.WriteLine("+-----+------------------------------+");
                        foreach (tinhthanhpho ttp in getttpt)
                        {
                            Console.WriteLine("| {0, -4}| {1, -29}|", ttp.matp, ttp.name);
                            Console.WriteLine("+-----+------------------------------+");
                        }
                        Console.WriteLine("                             ({0}/{1})", pageNumber, totalPages);
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
                    int a = pageNumber;
                    int b = (a - 1) * pageSize;
                    int c = b + pageSize - 1;
                    List<tinhthanhpho> q = tvdeliveryaddress.GetTinh(pageSize, b);
                    var getma = q.Where(x => x.matp == input);
                    if (getma.Count() == 0)
                    {
                        Console.WriteLine("Not Found");
                        exit = false;
                    }
                    else
                    {
                        exit = true;
                    }
                }
            }
            return input;
        }
        public string GetHuyen(string matinh)
        {
            string input = "";
            TVDeliveryAddress tvdeliveryaddress = new TVDeliveryAddress();
            int pageNumber = 1;
            int pageSize = 10;
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = startIndex + pageSize - 1;
            bool exit = false;
            List<quanhuyen> lstquanhuyen = tvdeliveryaddress.GetHuyen(pageSize, startIndex, matinh);
            List<quanhuyen> demquanhuyen = tvdeliveryaddress.GetHuyen(100, 0, matinh);
            int count = demquanhuyen.Count();
            int totalProducts = count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            Console.WriteLine("+-----+------------------------------+");
            Console.WriteLine("| ID  | Distric                      |");
            Console.WriteLine("+-----+------------------------------+");
            foreach (quanhuyen qh in lstquanhuyen)
            {
                Console.WriteLine("| {0, -4}| {1, -29}|", qh.maqh, qh.name);
                Console.WriteLine("+-----+------------------------------+");
            }
            Console.WriteLine("                             ({0}/{1})", pageNumber, totalPages);
            while (!exit)
            {
                Console.WriteLine("Enter 'N' To Next Page");
                Console.WriteLine("Enter 'P' To Back Page");
                Console.WriteLine("Enter 'Q' To Exit");
                Console.WriteLine("Enter Number Is Distric Id");
                Console.Write("--> ");
                input = Console.ReadLine();
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
                        List<quanhuyen> getqh = tvdeliveryaddress.GetHuyen(pageSize, startIndex, matinh);
                        Console.WriteLine("+-----+------------------------------+");
                        Console.WriteLine("| ID  | Distric                      |");
                        Console.WriteLine("+-----+------------------------------+");
                        foreach (quanhuyen qh in getqh)
                        {
                            Console.WriteLine("| {0, -4}| {1, -29}|", qh.maqh, qh.name);
                            Console.WriteLine("+-----+------------------------------+");
                        }
                        Console.WriteLine("                             ({0}/{1})", pageNumber, totalPages);
                    }
                }
                else if (input.ToLower() == "p")
                {
                    if (pageNumber > 1)
                    {
                        pageNumber--;
                        startIndex = (pageNumber - 1) * pageSize;
                        endIndex = startIndex + pageSize - 1;
                        List<quanhuyen> getqht = tvdeliveryaddress.GetHuyen(pageSize, startIndex, matinh);
                        Console.WriteLine("+-----+------------------------------+");
                        Console.WriteLine("| ID  | Distric                      |");
                        Console.WriteLine("+-----+------------------------------+");
                        foreach (quanhuyen qh in getqht)
                        {
                            Console.WriteLine("| {0, -4}| {1, -29}|", qh.maqh, qh.name);
                            Console.WriteLine("+-----+------------------------------+");
                        }
                        Console.WriteLine("                             ({0}/{1})", pageNumber, totalPages);
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
                    int a = pageNumber;
                    int b = (a - 1) * pageSize;
                    int c = b + pageSize - 1;
                    List<quanhuyen> q = tvdeliveryaddress.GetHuyen(pageSize, b, matinh);
                    var getma = q.Where(x => x.maqh == input);
                    if (getma.Count() == 0)
                    {
                        Console.WriteLine("Not Found");
                        exit = false;
                    }
                    else
                    {
                        exit = true;
                    }
                }
            }
            return input;
        }
        public string GetPhuong(string mahuyen)
        {
            string input = "";
            TVDeliveryAddress tvdeliveryaddress = new TVDeliveryAddress();
            int pageNumber = 1;
            int pageSize = 10;
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = startIndex + pageSize - 1;
            bool exit = false;
            List<xaphuongthitran> lstxaphuongthitran = tvdeliveryaddress.GetPhuong(pageSize, startIndex, mahuyen);
            List<xaphuongthitran> demxaphuongthitran = tvdeliveryaddress.GetPhuong(100, 0, mahuyen);
            int count = demxaphuongthitran.Count();
            int totalProducts = count;
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            Console.WriteLine("+-----+------------------------------+");
            Console.WriteLine("| ID  | Commune                      |");
            Console.WriteLine("+-----+------------------------------+");
            foreach (xaphuongthitran xptt in lstxaphuongthitran)
            {
                Console.WriteLine("|{0, -5}| {1, -29}|", xptt.xaid, xptt.name);
                Console.WriteLine("+-----+------------------------------+");
            }
            Console.WriteLine("                             ({0}/{1})", pageNumber, totalPages);
            while (!exit)
            {
                Console.WriteLine("Enter 'N' To Next Page");
                Console.WriteLine("Enter 'P' To Back Page");
                Console.WriteLine("Enter 'Q' To Exit");
                Console.WriteLine("Enter Number Is Commune Id");
                Console.Write("--> ");
                input = Console.ReadLine();
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
                        List<xaphuongthitran> getxptt = tvdeliveryaddress.GetPhuong(pageSize, startIndex, mahuyen);
                        Console.WriteLine("+-----+------------------------------+");
                        Console.WriteLine("| ID  | Commune                      |");
                        Console.WriteLine("+-----+------------------------------+");
                        foreach (xaphuongthitran xptt in getxptt)
                        {
                            Console.WriteLine("|{0, -5}| {1, -29}|", xptt.xaid, xptt.name);
                            Console.WriteLine("+-----+------------------------------+");
                        }
                        Console.WriteLine("                             ({0}/{1})", pageNumber, totalPages);
                    }
                }
                else if (input.ToLower() == "p")
                {
                    if (pageNumber > 1)
                    {
                        pageNumber--;
                        startIndex = (pageNumber - 1) * pageSize;
                        endIndex = startIndex + pageSize - 1;
                        List<xaphuongthitran> getxpttt = tvdeliveryaddress.GetPhuong(pageSize, startIndex, mahuyen);
                        Console.WriteLine("+-----+------------------------------+");
                        Console.WriteLine("| ID  | Commune                      |");
                        Console.WriteLine("+-----+------------------------------+");
                        foreach (xaphuongthitran xptt in getxpttt)
                        {
                            Console.WriteLine("|{0, -5}| {1, -29}|", xptt.xaid, xptt.name);
                            Console.WriteLine("+---- +------------------------------+");
                        }
                        Console.WriteLine("                             ({0}/{1})", pageNumber, totalPages);
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
                    int a = pageNumber;
                    int b = (a - 1) * pageSize;
                    int c = b + pageSize - 1;
                    List<xaphuongthitran> q = tvdeliveryaddress.GetPhuong(pageSize, b, mahuyen);
                    var getma = q.Where(x => x.xaid == input);
                    if (getma.Count() == 0)
                    {
                        Console.WriteLine("Not Found");
                        exit = false;
                    }
                    else
                    {
                        exit = true;
                    }
                }
            }
            return input;
        }
    }
}