using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_ASP.NET_NHOM_5.Models
{
    public class CartItem
    {
        public SanPham Product { set; get; }
        public int Quantity { set; get; }
    }
}