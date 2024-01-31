using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.Services;
using DAL.DBContext;

namespace BL.Controller
{
    public class CUDAccount
    {
        public void UpdatePhoneNumber(string account_username)
        {
            int so = 0;
            try
            {
                TVAccount tvacc = new TVAccount();
                do
                {
                    string value;
                    int sdt;
                    do
                    {
                        Console.Write("Enter New Phone Number: ");
                        value = Console.ReadLine();
                        if (Regex.IsMatch(value, @"^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$"))
                        {
                            sdt = 0;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Phone Number, Please Try Again");
                            sdt = 1;
                        }
                    } while (sdt != 0);
                    List<account> lst = tvacc.GetAccount();
                    var check = lst.Find(x => x.phonenumber == value);
                    if (check == null)
                    {
                        tvacc.UpdateAccountInfoPhone(account_username, value);
                        Console.WriteLine("Successfully Updated");
                        so = 0;
                    }
                    else
                    {
                        Console.WriteLine("Update Failed");
                        Console.WriteLine("The Phone Number You Entered Already exists");
                        do
                        {
                            Console.WriteLine("Do You Want To Try Again Or Exit?");
                            Console.WriteLine("1. Try Again");
                            Console.WriteLine("0. Exit");
                            Console.Write("--> ");
                            so = int.Parse(Console.ReadLine());
                            if (so != 0 && so != 1)
                            {
                                Console.WriteLine("Choice Again");
                            }
                        } while (so != 0 && so != 1);
                    }
                } while (so != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }
        public void UpdateEmail(string account_username)
        {
            int so = 0;
            try
            {
                TVAccount tvacc = new TVAccount();
                do
                {
                    int b;
                    string value;
                    do
                    {
                        Console.Write("Your email: ");
                        value = Console.ReadLine();
                        if (value.Contains("@") && value.IndexOf(" ") == -1)
                        {
                            b = 0;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Email, Please Try Again");
                            b = 1;
                        }
                    } while (b != 0);
                    List<account> lst = tvacc.GetAccount();
                    var check = lst.Find(x => x.email == value);
                    if (check == null)
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
                            else if (yn.ToLower() == "y")
                            {
                                tvacc.UpdateAccountInfoEmail(account_username, value);
                                Console.WriteLine("Successfully Updated");
                                so = 0;
                            }
                            else if (yn.ToLower() == "n")
                            {
                                so = 0;
                            }
                        } while (yn.ToLower() != "n" && yn.ToLower() != "y");
                    }
                    else
                    {
                        Console.WriteLine("Update Failed");
                        Console.WriteLine("The Email You Entered Already exists");
                        do
                        {
                            Console.WriteLine("Do You Want To Try Again Or Exit?");
                            Console.WriteLine("1. Try Again");
                            Console.WriteLine("0. Exit");
                            Console.Write("--> ");
                            so = int.Parse(Console.ReadLine());
                            if (so != 0 && so != 1)
                            {
                                Console.WriteLine("Choice Again");
                            }
                        } while (so != 0 && so != 1);
                    }
                } while (so != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }
        public void DeleteAccount(string username)
        {
            int up = 0;
            TVAccount delete = new TVAccount();
            try
            {
                do
                {
                    Console.WriteLine("To Delete An Account, You Need To Re-Enter The Username And Password");
                    Console.Write("Username: ");
                    username = Console.ReadLine();
                    CUDHidePassword hidepass = new CUDHidePassword();
                    Console.Write("Password: ");
                    string password = hidepass.GetPassword();
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
                            Console.Write("--> ");
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
                                delete.DeleteAccount(username);
                                Console.WriteLine("Delete Account Successful");
                                up = 0;
                            }
                            else if (yn.ToLower() == "n")
                            {
                                up = 0;
                            }
                        } while (yn.ToLower() != "n" && yn.ToLower() != "y");
                    }
                } while (up != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void UpdatePassword(string username)
        {
            int up = 0;
            CUDHidePassword cmk = new CUDHidePassword();
            TVAccount updatepass = new TVAccount();
            try
            {
                do
                {
                    Console.WriteLine("To Update Your Password, You Need To Re-Enter The Old Password");
                    string password;
                    do
                    {
                        Console.Write("Old Password: ");
                        password = cmk.GetPassword();
                        if (password.Length < 6)
                        {
                            Console.WriteLine("Password Must Greater 6 Characters");
                        }
                    } while (password.Length < 6);
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
                            Console.Write("--> ");
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
                        var chuoi = 0;
                        string value;
                        do
                        {
                            Console.Write("Enter New Password: ");
                            value = cmk.GetPassword();
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
                                updatepass.UpdateAccountInfoPass(username, value);
                                Console.WriteLine("Update Successful");
                                up = 0;
                            }
                            else if (yn.ToLower() == "n")
                            {
                                up = 0;
                            }
                        } while (yn.ToLower() != "n" && yn.ToLower() != "y");
                        up = 0;
                    }
                } while (up != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}