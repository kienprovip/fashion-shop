using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class variation
    {
        public int variation_id { get; set; }
        public int product_id { get; set; }
        public string color_name { get; set; }
        public string size_name { get; set; }
        public int product_quantity { get; set; }
        public string variation_TrangThai { get; set; }
    }
}
