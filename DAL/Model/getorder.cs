using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class getorder
    {
        public int order_id { get; set; }
        public string product_name { get; set; }
        public int variation_id { get; set; }
        public string account_username { get; set; }
        public string consignee_name { get; set; }
        public string consignee_phonenumber { get; set; }
        public string consignee_address { get; set; }
        public string color_name { get; set; }
        public string size_name { get; set; }
        public float order_price { get; set; }
        public DateTime order_datetime { get; set; }
        public string pay_name { get; set; }
        public int order_quantity { get; set; }
        public string order_TrangThai { get; set; }
    }
}