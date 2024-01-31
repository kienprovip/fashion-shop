using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class delivery_address
    {
        public int delivery_address_id { get; set; }
        public string consignee_name { get; set; }
        public string account_username { get; set; }
        public string consignee_phonenumber { get; set; }
        public string consignee_address { get; set; }
        public string delivery_address_TrangThai { get; set; }
        public string matp { get; set; }
        public string maqh { get; set; }
        public string xaid { get; set; }
        public string fulladdress { get; set; }
    }
}
