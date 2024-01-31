using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.Controller
{
    public class CUDProductVariation
    {
        public void Add()
        {
            try
            {
                TVProduct addproduct = new TVProduct();
                TVCategory addcategory = new TVCategory();
                TVSize addsize = new TVSize();
                TVColor addcolor = new TVColor();
                TVVariation addvariation = new TVVariation();
                TVProductVariation addproductvariation = new TVProductVariation();
                int product_ma;
                string color_ten = null;
                string size_ten = null;
                string category_ten = null;
                int Tid = 0;
                int Tdm = 0;
                int TCTGR = 0;
                int Ngia = 0;
                int bienthe = 1;
                do
                {
                    Console.Write("Enter ID: ");
                    product_ma = int.Parse(Console.ReadLine());
                    List<getproduct> lst = addproductvariation.GetAllHienthi();
                    var checkma = lst.Find(x => x.product_id == product_ma);
                    if (checkma == null)
                    {
                        Tid = 0;
                        Tdm = 1;
                        Ngia = 1;
                    }
                    else
                    {
                        Console.WriteLine("The ID You Entered Already exists");
                        do
                        {
                            Console.WriteLine("Do You Want To Try Again Or Exit?");
                            Console.WriteLine("1. Try Again");
                            Console.WriteLine("0. Exit");
                            Console.Write("--> ");
                            Tid = int.Parse(Console.ReadLine());
                            if (Tid != 0 && Tid != 1)
                            {
                                Console.WriteLine("Try Again");
                            }
                        } while (Tid != 1 && Tid != 0);
                        if (Tid == 0)
                        {
                            Tdm = 0;
                            Ngia = 0;
                        }
                    }
                } while (Tid != 0);
                if (Ngia == 1)
                {
                    Console.Write("Enter Product Name: ");
                    string product_ten = Console.ReadLine();
                    while (Tdm != 0)
                    {
                        Console.WriteLine("Which Category Would You Like To Add A Product To?");
                        Console.Write("Enter Category: ");
                        category_ten = Console.ReadLine();
                        List<category> lst = addcategory.GetAllCategory();
                        var checkcategory = lst.Find(x => x.category_name == category_ten);
                        if (checkcategory == null)
                        {
                            Console.WriteLine("Category You Entered Does Not Exist");
                            do
                            {
                                Console.WriteLine("Do You Want To Try Againt, Create New Category Or Exit");
                                Console.WriteLine("0. Exit");
                                Console.WriteLine("1. Creat New Category");
                                Console.WriteLine("2. Try Again");
                                Console.Write("--> ");
                                Tdm = int.Parse(Console.ReadLine());
                                if (Tdm != 0 && Tdm != 1 && Tdm != 2)
                                {
                                    Console.WriteLine("Try Again");
                                }
                            } while (Tdm != 1 && Tdm != 0 && Tdm != 2);
                            if (Tdm == 0)
                            {
                                Ngia = 0;
                            }
                            if (Tdm == 1)
                            {
                                do
                                {
                                    Console.Write("Enter Category Name You Want To Create: ");
                                    category_ten = Console.ReadLine();
                                    List<category> lstt = addcategory.GetAllCategory();
                                    var checktcategory = lstt.Find(x => x.category_name == category_ten);
                                    if (checktcategory == null)
                                    {
                                        addcategory.AddCategory(category_ten);
                                        Console.WriteLine("Successful");
                                        TCTGR = 0;
                                        Tdm = 0;
                                        Ngia = 1;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The Category Name You Entered Already exists");
                                        do
                                        {
                                            Console.WriteLine("Do You Want To Try Again Or Exit?");
                                            Console.WriteLine("1. Try Again");
                                            Console.WriteLine("0. Exit");
                                            Console.Write("--> ");
                                            TCTGR = int.Parse(Console.ReadLine());
                                            if (TCTGR != 0 && TCTGR != 1)
                                            {
                                                Console.WriteLine("Choice Again");
                                            }
                                        } while (TCTGR != 1 && TCTGR != 0);
                                    }
                                } while (TCTGR != 0);
                            }
                        }
                        else
                        {
                            Tdm = 0;
                            Ngia = 1;
                        }
                    }
                    if (Ngia == 1)
                    {
                        float product_gia;
                        do
                        {
                            Console.Write("Enter Price: ");
                            product_gia = float.Parse(Console.ReadLine());
                            if (product_gia <= 0)
                            {
                                Console.WriteLine("Price Must Greater 1");
                            }
                        } while (product_gia <= 0);
                        string product_trangthai = "on";
                        do
                        {
                            Console.Write("How Many Variations Do You Want To Add?: ");
                            bienthe = int.Parse(Console.ReadLine());
                            if (bienthe <= 0)
                            {
                                Console.WriteLine("Variation > 0, Please Try Again");
                            }
                        } while (bienthe <= 0);
                        for (int i = 1; i <= bienthe; i++)
                        {
                            int TCLR = 1;
                            int TSZE = 1;
                            int Dcl = 1;
                            int Dsz = 1;
                            Console.WriteLine("Variation {0}", i);
                            while (Dcl != 0)
                            {
                                Console.Write("Enter Color: ");
                                color_ten = Console.ReadLine();
                                List<color> lstcolor = addcolor.GetColor();
                                var checkcolor = lstcolor.Find(x => x.color_name == color_ten);
                                if (checkcolor == null)
                                {
                                    Console.WriteLine("Color You Entered Does Not Exist");
                                    do
                                    {
                                        Console.WriteLine("Do You Want To Try Againt, Create New Color Or Exit");
                                        Console.WriteLine("0. Exit");
                                        Console.WriteLine("1. Creat New Color");
                                        Console.WriteLine("2. Try Again");
                                        Console.Write("--> ");
                                        Dcl = int.Parse(Console.ReadLine());
                                        if (Dcl != 0 && Dcl != 1 && Dcl != 2)
                                        {
                                            Console.WriteLine("Choice Again");
                                        }
                                    } while (Dcl != 1 && Dcl != 0 && Dcl != 2);
                                    if (Dcl == 0)
                                    {
                                        Ngia = 0;
                                        Dsz = 0;
                                        i = bienthe + 1;
                                    }
                                    else if (Dcl == 1)
                                    {
                                        do
                                        {
                                            Console.Write("Enter Color You Want To Create: ");
                                            color_ten = Console.ReadLine();
                                            List<color> lsttcolor = addcolor.GetColor();
                                            var checktcolor = lsttcolor.Find(x => x.color_name == color_ten);
                                            if (checktcolor == null)
                                            {
                                                addcolor.AddColor(color_ten);
                                                Console.WriteLine("Successful");
                                                TCLR = 0;
                                                Dcl = 0;
                                                Ngia = 1;
                                                Dsz = 1;
                                            }
                                            else
                                            {
                                                Console.WriteLine("The Color Name You Entered Already exists");
                                                do
                                                {
                                                    Console.WriteLine("Do You Want To Try Again Or Exit?");
                                                    Console.WriteLine("1. Try Again");
                                                    Console.WriteLine("0. Exit");
                                                    Console.Write("--> ");
                                                    TCLR = int.Parse(Console.ReadLine());
                                                    if (TCLR != 0 && TCLR != 1)
                                                    {
                                                        Console.WriteLine("Choice Again");
                                                    }
                                                } while (TCLR != 1 && TCLR != 0);
                                            }
                                        } while (TCLR != 0);
                                    }
                                }
                                else
                                {
                                    Dcl = 0;
                                    Dsz = 1;
                                    Ngia = 1;
                                }
                            }
                            while (Dsz != 0)
                            {
                                Console.Write("Enter Size: ");
                                size_ten = Console.ReadLine();
                                List<size> lstsize = addsize.GetSize();
                                var checksize = lstsize.Find(x => x.size_name == size_ten);
                                if (checksize == null)
                                {
                                    Console.WriteLine("Size You Entered Does Not Exist");
                                    do
                                    {
                                        Console.WriteLine("Do You Want To Try Againt, Create New Size Or Exit");
                                        Console.WriteLine("0. Exit");
                                        Console.WriteLine("1. Creat New Size");
                                        Console.WriteLine("2. Try Again");
                                        Console.Write("--> ");
                                        Dsz = int.Parse(Console.ReadLine());
                                        if (Dsz != 0 && Dsz != 1 && Dsz != 2)
                                        {
                                            Console.WriteLine("Choice Again");
                                        }
                                    } while (Dsz != 1 && Dsz != 0 && Dsz != 2);
                                    if (Dsz == 0)
                                    {
                                        Ngia = 0;
                                        i = bienthe + 1;
                                    }
                                    if (Dsz == 1)
                                    {
                                        do
                                        {
                                            Console.Write("Enter Size You Want to Create: ");
                                            size_ten = Console.ReadLine();
                                            List<size> lsttsize = addsize.GetSize();
                                            var checktsize = lsttsize.Find(x => x.size_name == size_ten);
                                            if (checktsize == null)
                                            {
                                                addsize.AddSize(size_ten);
                                                Console.WriteLine("Successful");
                                                TSZE = 0;
                                                Ngia = 1;
                                                Dsz = 0;
                                            }
                                            else
                                            {
                                                Console.WriteLine("The Size Name You Entered Already exists");
                                                do
                                                {
                                                    Console.WriteLine("Do You Want To Try Again Or Exit?");
                                                    Console.WriteLine("1. Try Again");
                                                    Console.WriteLine("0. Exit");
                                                    Console.Write("--> ");
                                                    TSZE = int.Parse(Console.ReadLine());
                                                    if (TSZE != 0 && TSZE != 1)
                                                    {
                                                        Console.WriteLine("Choice Again");
                                                    }
                                                } while (TSZE != 1 && TSZE != 0);
                                            }
                                        } while (TSZE != 0);
                                    }
                                }
                                else
                                {
                                    Dsz = 0;
                                    Ngia = 1;
                                }
                            }
                            if (i == 1 && Dcl == 0 && Dsz == 0 && Ngia == 1)
                            {
                                addproduct.AddProduct(product_ma, category_ten, product_ten, product_gia, product_trangthai);
                            }
                            if (Ngia == 1)
                            {
                                int product_soluong;
                                do
                                {
                                    Console.Write("Enter Quantity Of This Variation: ");
                                    product_soluong = int.Parse(Console.ReadLine());
                                    if (product_soluong < 0)
                                    {
                                        Console.WriteLine("Quantity must greater or equal 0");
                                    }
                                } while (product_soluong < 0);
                                string variation_trangthai = "on";
                                addvariation.AddVariation(product_ma, size_ten, color_ten, product_soluong, variation_trangthai);
                                Console.WriteLine("Successfully Added New Variable");
                            }
                        }
                        Console.WriteLine("Successfully Added New Product");
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