using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class payment
    {
        public string accounnt_username { get; set; }
        public string pay_name { get; set; }
        public int pay_id { get; set; }
        public string pay_number { get; set; }
        public int pay_password { get; set; }
    }
}