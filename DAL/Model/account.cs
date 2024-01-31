using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class account
    {
        public string account_username { get; set; }
        public string account_password { get; set; }
        public string name { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string account_TrangThai { get; set; }
    }
}