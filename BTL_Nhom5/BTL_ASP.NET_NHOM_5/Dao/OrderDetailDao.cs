using BTL_ASP.NET_NHOM_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_ASP.NET_NHOM_5.Dao
{
    public class OrderDetailDao
    {
        TrueMartDb db = null;
        public OrderDetailDao()
        {
            db = new TrueMartDb();
        }
        public void Insert(ChiTietHoaDon detail)
        {
                db.ChiTietHoaDons.Add(detail);
                db.SaveChanges();
               
        }
    }
}