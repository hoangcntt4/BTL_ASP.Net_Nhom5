using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_ASP.NET_NHOM_5.Models;

namespace BTL_ASP.NET_NHOM_5.Areas.Admin.Controllers
{
    public class ThongKesController : BaseController
    {
        // GET: Admin/ThongKes
        private TrueMartDb db = new TrueMartDb();
        public ActionResult Index()
        {
            Tab();
            DateTime datenow = DateTime.Parse(DateTime.Now.ToShortDateString());
            ViewBag.title_char1 = "Biểu đồ doanh thu 15 ngày gàn nhất";
            Char1(datenow.AddDays(-14), datenow);
            return View();
        }
        [HttpPost]
        public ActionResult Index(String start, String end)
        {
            if (start == "" || end == "")
                return RedirectToAction("Index", "ThongKes");
            Tab();
            DateTime datenow = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime dateS = DateTime.Parse(start);
            DateTime dateE = DateTime.Parse(end);
            ViewBag.title_char1 = "Biểu đồ doanh thu từ ngày " + start + " đến ngày " + end;
            Char1(dateS, dateE);
            Char2(datenow);
            Char3();
            return View();
        }
        private void Tab()
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var tong = db.ChiTietHoaDons.Where(t => t.MaHD == t.MaSP).Sum(t => t.SoLuong * t.DonGia);
            if (tong != null)
                ViewBag.tien_ht = String.Format("{0:0,0.00}", tong);
            else
                ViewBag.tien_ht = "0";
            ViewBag.so_hoa_don = db.HoaDons.Count();
            ViewBag.so_luong_sp = db.SanPhams.Count();
            ViewBag.so_luong_khach_hang = db.KhachHangs.Count();
            ViewBag.so_luong_km = db.KhuyenMais.Count();
        }
        private void Char1(DateTime start, DateTime end)
        {
            List<Double> C1sl = new List<Double>();
            List<String> C1name = new List<String>();
            int num = (end - start).Days;
            double tong = 0;
            for (int i = 0; i <= num; i++)
            {
                DateTime f1 = end.AddDays(-num + i);
                DateTime f2 = f1.AddDays(1);
                var q = db.ChiTietHoaDons.Where(t => t.MaHD == t.MaSP).Sum(t => t.SoLuong * t.DonGia);
                if (q == null)
                    q = 0;
                tong += (double)q;
                C1sl.Add((Double)q);
                C1name.Add(f1.Day.ToString() + " / " + f1.Month.ToString());
            }
            ViewBag.tong_tien = "Tổng doanh thu từ ngày " + start.ToShortDateString() + " tới ngày " + end.ToShortDateString() + " là " + String.Format("{0:0,0.00}", tong) + " VND";
            ViewBag.C1sl = C1sl;
            ViewBag.C1name = C1name;
        }
        private void Char2(DateTime end)
        {
            List<int> C2sl = new List<int>();
            List<String> C2name = new List<String>();
            System.Diagnostics.Debug.WriteLine("DAY 2: " + end);
            for (int i = 1; i <= 7; i++)
            {
                DateTime f1 = end.AddDays(-7 + i);
                DateTime f2 = f1.AddDays(1);
                var q = db.HoaDons.Count();
                C2sl.Add(q);
                C2name.Add(f1.Day.ToString() + " / " + f1.Month.ToString());
            }
            ViewBag.C2sl = C2sl;
            ViewBag.C2name = C2name;
        }
        private void Char3()
        {
            var s = db.KhuyenMais.GroupBy(t => t.TenKM).Select(t => new { ten_km = t.Key, total = t.Sum(i => i.SanPhams.Count) });
            List<String> name = new List<String>();
            List<int> total = new List<int>();
            foreach (var group in s)
            {
                System.Diagnostics.Debug.WriteLine("Mã khuyến mại: " + group.ten_km + " | SL: " + group.total);
                name.Add((String)group.ten_km);
                total.Add((int)group.total);
            }
            ViewBag.name = name;
            ViewBag.total = total;
        }
    }
}
