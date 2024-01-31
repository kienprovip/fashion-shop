using System.Text.RegularExpressions;
using BL.Controller;
using DAL.Services;
using DAL.DBContext;

namespace BL.Controller
{
    public class Register
    {
        public void DangKy()
        {
            CUDHidePassword cmk = new CUDHidePassword();
            string username = null;
            string password = null;
            string name = null;
            string phone = null;
            string Email = null;
            int Duser = 0;
            int Dphone = 0;
            int Demail = 0;
            do
            {
                TVAccount addaccount = new TVAccount();
                List<account> lst = addaccount.GetAccount();
                var chuoi = 0;
                do
                {
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    if (username.Length < 6)
                    {
                        Console.WriteLine("The Username Must Be Greater Than Or Equal To 6 Characters");
                        chuoi = 1;
                    }
                    else
                    {
                        chuoi = 0;
                    }
                } while (chuoi != 0);
                var checkusername = lst.Find(x => x.account_username == username);
                if (checkusername == null)
                {
                    Duser = 0;
                    Dphone = 1;
                }
                else
                {
                    do
                    {
                        Console.WriteLine("The Username Already Exists");
                        Console.WriteLine("Do You Want To Exit?");
                        Console.WriteLine("0. Exit");
                        Console.WriteLine("1. Try Again");
                        Console.Write("--> ");
                        Duser = int.Parse(Console.ReadLine());
                        if (Duser != 0 && Duser != 1)
                        {
                            Console.WriteLine("Choice Again");
                        }
                    } while (Duser != 0 && Duser != 1);
                    if (Duser == 0)
                    {
                        Dphone = 0;
                        Demail = 0;
                    }
                }
            } while (Duser != 0);
            if (Dphone != 0)
            {
                var chuoi = 0;
                do
                {
                    Console.Write("Password: ");
                    password = cmk.GetPassword();
                    if (password.Length < 6)
                    {
                        Console.WriteLine("The Password Must Be Greater Than Or Equal To 6 Characters");
                        chuoi = 1;
                    }
                    else
                    {
                        chuoi = 0;
                    }
                } while (chuoi != 0);
                int c;
                do
                {
                    Console.Write("Your Name: ");
                    name = Console.ReadLine();
                    if (Regex.IsMatch(name, "^[a-z ]+$"))
                    {
                        c = 0;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Name, Please Try Again");
                        c = 1;
                    }
                } while (c != 0);
            }
            while (Dphone != 0)
            {
                int a;
                do
                {
                    Console.Write("Your Phone Number: ");
                    phone = Console.ReadLine();
                        if (Regex.IsMatch(phone, @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$"))
                        {
                            a = 0;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Phone Number, Please Try Again");
                            a = 1;
                        }
                    } while (a != 0) ;
                    TVAccount addaccount = new TVAccount();
                    List<account> lst = addaccount.GetAccount();
                    if (lst.Find(x => x.phonenumber == phone) == null)
                    {
                        Dphone = 0;
                        Demail = 1;
                    }
                    else
                    {
                        do
                        {
                            Console.WriteLine("The Phone Number Already Exists");
                            Console.WriteLine("Do You Want To Exit?");
                            Console.WriteLine("0. Exit");
                            Console.WriteLine("1. Try Again");
                            Console.Write("--> ");
                            Dphone = int.Parse(Console.ReadLine());
                            if (Dphone != 0 && Dphone != 1)
                            {
                                Console.WriteLine("Choice Again");
                            }
                        } while (Dphone != 0 && Dphone != 1);
                        if (Dphone == 0)
                        {
                            Demail = 0;
                        }
                    }
                }
                while (Demail != 0)
            {
                    int b;
                    do
                    {
                        Console.Write("Your email: ");
                        Email = Console.ReadLine();
                        if (Email.Contains("@") && Email.IndexOf(" ") == -1)
                        {
                            b = 0;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Email, Please Try Again");
                            b = 1;
                        }
                    } while (b != 0);
                    TVAccount addaccount = new TVAccount();
                    List<account> lst = addaccount.GetAccount();
                    if (lst.Find(x => x.email == Email) == null)
                    {
                        addaccount.AddUser(username, password, name, phone, Email);
                        TVPay tvpay = new TVPay();
                        string pay_ten = "Pay By Cash";
                        tvpay.AddPayment(username, 8, "1111", 123456, pay_ten);
                        Console.WriteLine("Account Creation Successful");
                        Demail = 0;
                    }
                    else
                    {
                        do
                        {
                            Console.WriteLine("The Email Already Exists");
                            Console.WriteLine("Do You Want To Exit");
                            Console.WriteLine("0. Exit");
                            Console.WriteLine("1. Try Again");
                            Console.Write("--> ");
                            Demail = int.Parse(Console.ReadLine());
                            if (Demail != 0 && Demail != 1)
                            {
                                Console.WriteLine("Choice Again");
                            }
                        } while (Demail != 0 && Demail != 1);
                    }
                }
            }
        }
    }