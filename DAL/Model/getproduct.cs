using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class getproduct
    {
        public int product_id { get; set; }
        public string category_name{ get; set; }
        public string product_name { get; set; }
        public string size_name { get; set; }
        public string color_name { get; set; }
        public float product_price { get; set; }
        public int product_quantity { get; set; }
        public string product_TrangThai { get; set; }
        public string variation_TrangThai { get; set; }
    }
}