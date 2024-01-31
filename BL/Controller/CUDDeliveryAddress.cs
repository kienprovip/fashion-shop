using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;
using BL.View;

namespace BL.Controller
{
    public class CUDDeliveryAddress
    {
        public void Add(string account_username)
        {
            try
            {
                var YN = "";
                do
                {
                    string consignee_phonenumber;
                    Console.WriteLine("ADD NEW DELIVERY ADDRESS");
                    TVDeliveryAddress tv = new TVDeliveryAddress();
                    Console.Write("Enter Consignee Name: ");
                    string consignee_name = Console.ReadLine();
                    int sdt;
                    do
                    {
                        Console.Write("Enter Consignee Phone Number: ");
                        consignee_phonenumber = Console.ReadLine();
                        if (Regex.IsMatch(consignee_phonenumber, @"^\d{10}$"))
                        {
                            sdt = 0;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Phone Number, Please Try Again");
                            sdt = 1;
                        }
                    } while (sdt != 0);
                    DiaChi diachi = new DiaChi();
                    string matinh = diachi.GetTinh();
                    string mahuyen = diachi.GetHuyen(matinh);
                    string maxa = diachi.GetPhuong(mahuyen);
                    Console.Write("Enter Your Specific Address: ");
                    string consignee_address = Console.ReadLine();
                    do
                    {
                        Console.Write("Are You Sure?(Y/N): ");
                        YN = Console.ReadLine();
                        if (YN.ToLower() == "y")
                        {
                            tv.AddAddress(account_username, consignee_name, consignee_phonenumber, matinh, mahuyen, maxa, consignee_address);
                        }
                        else if (YN.ToLower() != "n" && YN.ToLower() != "y")
                        {
                            Console.WriteLine("Try Again");
                        }
                    } while (YN.ToLower() != "n" && YN.ToLower() != "y");
                } while (YN.ToLower() != "n" && YN.ToLower() != "y");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }
        public void Update(string tdn)
        {
            TVDeliveryAddress update = new TVDeliveryAddress();
            try
            {
                var YN = "";
                string consignee_sdt;
                List<delivery_address> deliveryList = update.GetAllDeliveryAddress(tdn);
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
                int chonma;
                int count = 0;
                do
                {
                    Console.Write("Enter Delivery Address ID: ");
                    chonma = int.Parse(Console.ReadLine());
                    var ktma = deliveryList.Where(x => x.delivery_address_id == chonma && x.delivery_address_TrangThai == "on");
                    if (ktma.Count() == 0)
                    {
                        Console.WriteLine("Not Found");
                        do
                        {
                            Console.WriteLine("Do You Want To Exit?");
                            Console.WriteLine("0. Exit");
                            Console.WriteLine("1. Re-Enter");
                            Console.Write("--> ");
                            count = int.Parse(Console.ReadLine());
                            if (count != 0 && count != 1)
                            {
                                Console.WriteLine("Try Again");
                            }
                        } while (count != 0 && count != 1);
                    }
                    else
                    {
                        Console.Write("Enter New Consignee Name: ");
                        string consignee_ten = Console.ReadLine();
                        int sdt;
                        do
                        {
                            Console.Write("Enter New Consignee Phone Number: ");
                            consignee_sdt = Console.ReadLine();
                            if (Regex.IsMatch(consignee_sdt, @"^\d{10}$"))
                            {
                                sdt = 0;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Phone Number, Please Try Again");
                                sdt = 1;
                            }
                        } while (sdt != 0);
                        DiaChi diachi = new DiaChi();
                        string matinh = diachi.GetTinh();
                        string mahuyen = diachi.GetHuyen(matinh);
                        string maxa = diachi.GetPhuong(mahuyen);
                        Console.Write("Enter Your Specific Address: ");
                        string consignee_address = Console.ReadLine();
                        do
                        {
                            Console.Write("Are You Sure?(Y/N): ");
                            YN = Console.ReadLine();
                            if (YN.ToLower() == "y")
                            {
                                update.UpdateAddress(consignee_ten, consignee_sdt, matinh, mahuyen, maxa, consignee_address, chonma, tdn);
                                count = 0;
                            }
                            else if (YN.ToLower() != "n" && YN.ToLower() != "y")
                            {
                                Console.WriteLine("Try Again");
                            }
                            else if (YN.ToLower() == "n")
                            {
                                count = 0;
                            }
                        } while (YN.ToLower() != "n" && YN.ToLower() != "y");
                    }
                } while (count != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }
        public void Delete(string tdn)
        {
            TVDeliveryAddress delete = new TVDeliveryAddress();
            try
            {
                List<delivery_address> deliveryList = delete.GetAllDeliveryAddress(tdn);
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
                int chonma;
                int count = 0;
                do
                {
                    Console.Write("Enter The Delivery Address ID You Want To Delete: ");
                    chonma = int.Parse(Console.ReadLine());
                    var ktma = deliveryList.Where(x => x.delivery_address_id == chonma && x.delivery_address_TrangThai == "on");
                    if (ktma.Count() == 0)
                    {
                        Console.WriteLine("Not Found");
                        do
                        {
                            Console.WriteLine("Do You Want To Exit?");
                            Console.WriteLine("0. Exit");
                            Console.WriteLine("1. Re-Enter");
                            Console.Write("--> ");
                            count = int.Parse(Console.ReadLine());
                            if (count != 0 && count != 1)
                            {
                                Console.WriteLine("Try Again");
                            }
                        } while (count != 0 && count != 1);
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
                                delete.DeleteAddress(chonma, tdn);
                                count = 0;
                            }
                            else if (YN.ToLower() != "n" && YN.ToLower() != "y")
                            {
                                Console.WriteLine("Try Again");
                            }
                            else if (YN.ToLower() == "n")
                            {
                                count = 0;
                            }
                        } while (YN.ToLower() != "n" && YN.ToLower() != "y");
                    }
                } while (count != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }
    }
}