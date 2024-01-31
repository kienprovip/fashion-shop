using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.Controller
{
    public class CUDOrderPay
    {
        public void CheckNewOrder()
        {
            TVProductVariation tvproductvariation = new TVProductVariation();
            List<getproduct> lstproductvariation = tvproductvariation.GetAllHienthi();
            TVOrderPayAddress tvod = new TVOrderPayAddress();
            List<getorder> geto = tvod.GetAllOder();
            var result = geto.Where(x => x.order_TrangThai == "processing");
            if (result.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
                Console.Write("Enter Any Key To Exit...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                Console.WriteLine("| ID |Product Name| Name     |Phonenumber| Consignee Address                                        |Color |Size|Price($)|Order Datetime        |Pay Name   |Quantity|Status     |");
                Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                foreach (getorder od in result)
                {
                    Console.WriteLine("| {0,-3}| {1,-11}| {2,-9}| {3,-10}|{4,-58}|{5,-6}|{6,-4}| {7,-7}|{8,-22}|{9,-11}| {10,-7}| {11, -10}|", od.order_id, od.product_name, od.consignee_name, od.consignee_phonenumber, od.consignee_address, od.color_name, od.size_name, od.order_price, od.order_datetime, od.pay_name, od.order_quantity, od.order_TrangThai);
                    Console.WriteLine("+----+------------+----------+-----------+----------------------------------------------------------+------+----+--------+----------------------+-----------+--------+-----------+");
                }
                int chonmadonhang = 0;
                do
                {
                    Console.Write("Enter The Order Id You Want To Accept To Delivery: ");
                    int madonhang = int.Parse(Console.ReadLine());
                    var kiemtramadonhang = result.Where(x => x.order_id == madonhang);
                    if (kiemtramadonhang.Count() == 0)
                    {
                        Console.WriteLine("Not Found");
                        do
                        {
                            Console.WriteLine("Do You Want To Exit?");
                            Console.WriteLine("0. Exit");
                            Console.WriteLine("1. Re-Enter");
                            Console.Write("--> ");
                            chonmadonhang = int.Parse(Console.ReadLine());
                            if (chonmadonhang != 0 && chonmadonhang != 1)
                            {
                                Console.WriteLine("Try Again");
                            }
                        } while (chonmadonhang != 0 && chonmadonhang != 1);
                    }
                    else
                    {
                        string trangthai = "";
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
                                string trangthaivariation = "";
                                TVOder tvorder = new TVOder();
                                TVVariation tvvariation = new TVVariation();
                                List<variation> lstvariation = tvvariation.GetAllVariation();
                                int soluongdathang = geto.Where(x => x.order_id == madonhang && x.order_TrangThai == "processing").Select(x => x.order_quantity).FirstOrDefault();
                                int idbienthedat = geto.Where(x => x.order_id == madonhang).Select(x => x.variation_id).FirstOrDefault();
                                int soluongdangco = lstvariation.Where(x => x.variation_id == idbienthedat).Select(x => x.product_quantity).FirstOrDefault();
                                int soluongconlai = soluongdangco - soluongdathang;
                                if (soluongconlai == 0)
                                {
                                    trangthaivariation = "off";
                                }
                                else
                                {
                                    trangthaivariation = "on";
                                }
                                trangthai = "done";
                                tvvariation.UpdateQuantity(soluongconlai, trangthaivariation, idbienthedat);
                                tvorder.AcceptOrder(trangthai, madonhang);
                                chonmadonhang = 0;
                            }
                            else if (yn.ToLower() == "n")
                            {
                                chonmadonhang = 0;
                            }
                        } while (yn.ToLower() != "n" && yn.ToLower() != "y");
                    }

                } while (chonmadonhang != 0);
            }
        }
        public void RevenueThisMonth()
        {
            TVOrderPayAddress tvorderpayaddress = new TVOrderPayAddress();
            List<getorder> lstorderpayaddress = tvorderpayaddress.GetAllOder();
            var month = lstorderpayaddress.Where(x => x.order_TrangThai == "done").Select(x => x.order_datetime.Month).FirstOrDefault();
            var year = lstorderpayaddress.Where(x => x.order_TrangThai == "done").Select(x => x.order_datetime.Year).FirstOrDefault();
            float result = lstorderpayaddress.Where(x => x.order_TrangThai == "done" && x.order_datetime.Year == year && x.order_datetime.Month == month).Sum(x => x.order_price);
            Console.WriteLine("Revenue Of This Month Is: {0}$", result);
            TVOder tvorder = new TVOder();
            List<getorder> lstsold = tvorder.ProductSold();
            var lstsoldct = lstsold.OrderByDescending(x => x.order_price).ToList();
            float a = 100;
            if (result != 0)
            {
                Console.WriteLine("+------------+-----------+------------+");
                Console.WriteLine("|Product Name|Revenue($) | Percent(%) |");
                Console.WriteLine("+------------+-----------+------------+");
                foreach (getorder od in lstsoldct)
                {
                    Console.WriteLine("| {0,-11}| {1,-10}| {2,-11}|", od.product_name, od.order_price, ((od.order_price * a) / result).ToString("0.00"));
                    Console.WriteLine("+------------+-----------+------------+");
                }
            }
        }
        public void ProductSold()
        {
            TVOder tvorderpayaddress = new TVOder();
            List<getorder> lstorderpayaddress = tvorderpayaddress.ProductSold();
            var result = lstorderpayaddress.OrderByDescending(x => x.order_quantity).ToList();
            if (lstorderpayaddress.Count() == 0)
            {
                Console.WriteLine("List Is Empty");
            }
            else
            {
                Console.WriteLine("THE PRODUCT SOLD IN THE MONTH");
                Console.WriteLine("+------------+--------+");
                Console.WriteLine("|Product Name|Quantity|");
                Console.WriteLine("+------------+--------+");
                foreach (getorder od in result)
                {
                    Console.WriteLine("| {0,-11}| {1,-7}|", od.product_name, od.order_quantity);
                    Console.WriteLine("+------------+--------+");
                }
                var maxslg = lstorderpayaddress.Max(x => x.order_quantity);
                var flashsalename = lstorderpayaddress.Where(x => x.order_quantity == maxslg);
                if (flashsalename.Count() == 1)
                {
                    foreach (getorder getod in flashsalename)
                    {
                        Console.WriteLine("Flash Sale Is: {0}", getod.product_name);
                    }
                }
                else
                {
                    Console.Write("Flash Sale Are: ");
                    foreach (getorder getod in flashsalename)
                    {
                        Console.Write("{0}, ", getod.product_name);
                    }
                }
            }
        }
    }
}