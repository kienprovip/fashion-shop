using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class orders { 
        public int order_id { get; set; }
        public string account_username { get; set; }
        public int variation_id { get; set; }
        public int delivery_address_id { get; set; }
        public int product_id { get; set; }
        public DateTime order_datetime { get; set; }
        public int order_quantity { get; set; }
        public float order_price { get; set; }
        public string order_TrangThai { get; set; }
    }
}
