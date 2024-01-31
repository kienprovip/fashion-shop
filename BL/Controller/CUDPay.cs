using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BL.View;
using DAL.Services;
using DAL.DBContext;

namespace BL.Controller
{
    public class CUDPay
    {
        public void AddPay(string user)
        {
            TVPay tvpay = new TVPay();
            List<pay> lstpay = tvpay.GetPay();
            ThanhToan tt = new ThanhToan();
            tt.GetAll();
            int chonma = 0;
            do
            {
                Console.Write("Enter The Pay Id You Want To Create: ");
                int pay_ma = int.Parse(Console.ReadLine());
                var kiemtrama = lstpay.Where(x => x.pay_id == pay_ma);
                if (kiemtrama.Count() == 0)
                {
                    Console.WriteLine("Not Found");
                    do
                    {
                        Console.WriteLine("Do You Want To Re-Enter?");
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
                    string pay_so;
                    do
                    {
                        Console.Write("Enter Your Card Number: ");
                        pay_so = Console.ReadLine();
                        if (!Regex.IsMatch(pay_so, "^[0-9]+$"))
                        {
                            Console.WriteLine("Try Again");
                        }
                    } while (!Regex.IsMatch(pay_so, "^[0-9]+$"));
                    int pay_matkhau;
                    do
                    {
                        Console.Write("Enter Your Password(6 Numbers): ");
                        pay_matkhau = int.Parse(Console.ReadLine());
                        if (pay_matkhau > 999999 || pay_matkhau < 100000)
                        {
                            Console.WriteLine("Try Again");
                        }
                    } while (pay_matkhau > 999999 || pay_matkhau < 100000);
                    string pay_ten = "Pay By Card";
                    List<payment> lstpayment = tvpay.GetPayment();
                    var kiemtrathanhtoan = lstpayment.Where(x => x.accounnt_username == user);
                    if (kiemtrathanhtoan.Count() == 2)
                    {
                        Console.WriteLine("Your Pay By Card Already Exit");

                        Console.Write("Enter Any Key To Exit...");
                        Console.ReadKey();
                        chonma = 0;
                    }
                    else if (kiemtrathanhtoan.Count() == 1)
                    {
                        var YN = "";
                        do
                        {
                            Console.Write("Are You Sure?(Y/N): ");
                            YN = Console.ReadLine();
                            if (YN.ToLower() == "y")
                            {
                                tvpay.AddPayment(user, pay_ma, pay_so, pay_matkhau, pay_ten);
                                Console.WriteLine("Sucessful");
                                chonma = 0;
                            }
                            else if (YN.ToLower() != "n" && YN.ToLower() != "y")
                            {
                                Console.WriteLine("Try Again");
                            }
                            else if (YN.ToLower() == "n")
                            {
                                chonma = 0;
                            }
                        } while (YN.ToLower() != "n" && YN.ToLower() != "y");
                    }
                }
            } while (chonma != 0);
        }
    }
}