using BTL_ASP.NET_NHOM_5.Common;
using BTL_ASP.NET_NHOM_5.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_ASP.NET_NHOM_5.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        // GET: Order
        public ActionResult DanhSachDonHang(string MaDH="", int page = 1, int pageSize = 2)
        {
            var user = Session[CommonConstants.USER_SESSION] as UserLogin;
            if (user != null)
            {
                TrueMartDb db = new TrueMartDb();
                var orders = db.HoaDons.Where(x => x.ID == user.UserID).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
               
                if(!string.IsNullOrEmpty(MaDH))
                {
                    int mahd = int.Parse(MaDH);
                    ViewBag.MaDH = MaDH;
                    orders = orders.Where(x => x.MaHD == mahd).ToPagedList(page, pageSize);
                }    
                return View(orders);
            }
            else return RedirectToAction("Logout", "User");


        }


        public ActionResult ChiTietHoaDon(int id)
        {

            TrueMartDb db = new TrueMartDb();
            var chitiets = db.ChiTietHoaDons.Where(x => x.MaHD == id).ToList();
            ViewBag.MaHD = id;
            return View(chitiets);


        }
    }
}