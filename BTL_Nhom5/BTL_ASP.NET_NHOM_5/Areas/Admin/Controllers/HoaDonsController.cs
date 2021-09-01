using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_ASP.NET_NHOM_5.Models;
using BTL_ASP.NET_NHOM_5.Areas.Admin.Models;
using BTL_ASP.NET_NHOM_5.Common;

namespace BTL_ASP.NET_NHOM_5.Areas.Admin.Controllers
{
    public class HoaDonsController : BaseController
    {
        private TrueMartDb db = new TrueMartDb();

        // GET: Admin/HoaDons
        public ActionResult Index()
        {
          
            var hoaDons = db.HoaDons.Include(h => h.KhachHang).Include(h => h.ChiTietHoaDons);
            double tong = 0;
            foreach (var item in hoaDons.ToList())
            {
                if (item.TrangThai.Contains("Đã nhận hàng"))
                {
                    tong += (double)item.ChiTietHoaDons.Where(t =>t.MaHD ==t.MaSP).Sum(t => t.SoLuong * t.DonGia);
                }
         
            }
            ViewBag.tong_tien = String.Format("{0:0,0.00}", tong);
            return View(hoaDons.ToList());
        }
        [HttpPost]
        public ActionResult Index(String beginDate, String endDate)
        {
            System.Diagnostics.Debug.WriteLine("your message here " + beginDate);
            List<HoaDon> dshd = new List<HoaDon>();
            String query = "select * from HoaDon where";
            if (!beginDate.Equals(""))
                query += "  CreatedDate >= '" + beginDate + "'";
            if (!endDate.Equals("") && !beginDate.Equals(""))
                query += " and CreatedDate <= '" + endDate + "'";
            else
                query += " CreatedDate <= '" + endDate + "'";

            dshd = db.HoaDons.SqlQuery(query).ToList();
            double tong = 0;
            foreach (var item in dshd)
            {
                if (item.TrangThai.Contains("Đã nhận hàng"))
                {
                    tong += (double)item.ChiTietHoaDons.Where(t => t.MaHD == t.MaSP).Sum(t=>t.SoLuong*t.DonGia);
                }
            }
            ViewBag.tong_tien = tong.ToString("C");
            return View(dshd);
        }

        // GET: Admin/HoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.QuanTris, "ID", "HoTen");
            ViewBag.MaHD = new SelectList(db.ChiTietHoaDons, "MaHD", "Sum(SoLuong*DonGia)");
            ViewBag.TinhTrang = new SelectList(db.HoaDons, "TinhTrang", "CreatedDate");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,TrangThai,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,ID")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.QuanTris, "ID", "HoTen",hoaDon.ID);
            ViewBag.MaHD = new SelectList(db.ChiTietHoaDons, "MaHD", "Sum(SoLuong*DonGia)", hoaDon.MaHD);
            ViewBag.TinhTrang = new SelectList(db.HoaDons, "TinhTrang", "CreatedDate", hoaDon.TrangThai);
            return View(hoaDon);
        }
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chitietdonhang = db.ChiTietHoaDons.Find(id);
            if (chitietdonhang == null)
            {
                return HttpNotFound();
            }
            return View(chitietdonhang);
        }
        // GET: Admin/HoaDons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            
            var kh = db.KhachHangs.Where(p => p.ID == hoaDon.ID).FirstOrDefault();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION_ADMIN];
            hoaDon.ModifiedDate = DateTime.Now;
            hoaDon.ModifiedBy = session.UserName;
            ViewBag.HoTenKhachHang = kh.HoTen;
            ViewBag.ID = new SelectList(db.QuanTris, "ID", "HoTen", hoaDon.ID);
            ViewBag.MaHD = new SelectList(db.ChiTietHoaDons, "MaHD", "Sum(SoLuong*DonGia)", hoaDon.MaHD);
            ViewBag.TinhTrang = new SelectList(db.HoaDons, "TinhTrang", "CreatedDate", hoaDon.TrangThai);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,TrangThai,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,ID")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.QuanTris, "ID", "HoTen", hoaDon.ID);
            ViewBag.MaHD = new SelectList(db.ChiTietHoaDons, "MaHD", "Sum(SoLuong*DonGia)", hoaDon.MaHD);
            ViewBag.TinhTrang = new SelectList(db.HoaDons, "TinhTrang", "CreatedDate", hoaDon.TrangThai);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Result(String ma_chitiethd, String hoten1, String hoten2, String hoten3, String hoten4, String tuoi1, String tuoi2, String tuoi3, String tuoi4)
        {
            if (ma_chitiethd == null)
            {
                return RedirectToAction("Index", "Index");
            }
            else
            {

            
              
                HoaDon hd = new HoaDon();
                hd.MaHD = Int32.Parse(ma_chitiethd);
                hd.TrangThai.Contains("Đã nhận hàng");
                try
                {
                    db.HoaDons.Add(hd);
                    HoaDon tgd = db.HoaDons.Find(Int32.Parse(ma_chitiethd));
                    if (tgd == null)
                    {
                        return HttpNotFound();
                    }
                    tgd.TrangThai.Contains("Đang vận chuyển");
                    db.Entry(tgd).State = EntityState.Modified;
                    ViewBag.ngay_ra = tgd.ModifiedDate;
                    db.SaveChanges();
                    ViewBag.Result = "success";
                }
                catch
                {
                    ViewBag.Result = "error";
                }
            }
            return View();
        }
        public ActionResult ChiTietHoaDon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon tblHoaDon = db.HoaDons.Find(id);
            if (tblHoaDon == null)
            {
                return HttpNotFound();
            }

            var tong_tien = (tblHoaDon.ChiTietHoaDons.Sum(t => t.SoLuong * t.DonGia)) ;
            ViewBag.t = tong_tien;

            ViewBag.time_now = DateTime.Now.ToString();

            List<SanPham> dsdv = db.SanPhams.Where(u => u.MaSP == id).Include(t => t.KhuyenMai).ToList();
            ViewBag.list_km = dsdv;
            double tongtienkm = 0;
            List<double> tt = new List<double>();
            foreach (var item in dsdv)
            {
                double t = (double)((double)(item.SoLuong*item.DonGia) * item.KhuyenMai.Discount);
                tongtienkm += t;
                tt.Add(t);
            }
            ViewBag.list_tt = tt;
            ViewBag.tien_khuyen_mai = tongtienkm;
            ViewBag.tong_tien_ = (double) tong_tien + tongtienkm;
            return View(tblHoaDon);
        }
       
    }
}
