using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class product
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string category_name { get; set; }
        public float product_price { get; set; }
        public string product_TrangThai { get; set; }
    }
}
