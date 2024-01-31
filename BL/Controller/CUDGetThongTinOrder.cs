using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;

namespace BL.Controller
{
    public class CUDGetThongTinOrder
    {
        public List<getthongtinorder> GetThongTinOrder(int order_ma, int product_ma, int variation_ma, int order_soluong, float order_gia)
        {
            List<getthongtinorder> lst = new List<getthongtinorder>();
            return lst;
        }
    }
}