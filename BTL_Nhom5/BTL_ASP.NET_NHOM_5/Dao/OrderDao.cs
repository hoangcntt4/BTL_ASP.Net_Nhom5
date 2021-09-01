using BTL_ASP.NET_NHOM_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_ASP.NET_NHOM_5.Dao
{
    public class OrderDao
    {
        TrueMartDb db = null;
        public OrderDao()
        {
            db = new TrueMartDb();
        }
        public int Insert(HoaDon order)
        {
            db.HoaDons.Add(order);
            db.SaveChanges();
            return db.HoaDons.OrderByDescending(obj => obj.MaHD).FirstOrDefault().MaHD;
        }
    }
}