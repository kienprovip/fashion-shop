using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class oders_pay
    {
        public int oder_id { get; set; }
        public int pay_id { get; set; }
        public int delivery_address_id { get; set; }
        public string orders_pay_TrangThai { get; set; }
    }
}
