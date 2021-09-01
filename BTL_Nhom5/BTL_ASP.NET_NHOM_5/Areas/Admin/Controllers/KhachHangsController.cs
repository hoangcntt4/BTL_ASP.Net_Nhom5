using BTL_ASP.NET_NHOM_5.Models;
using PagedList;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BTL_ASP.NET_NHOM_5.Areas.Admin.Controllers
{
    public class KhachHangsController : BaseController
    {
        private TrueMartDb db = new TrueMartDb();

        // GET: Admin/KhachHangs
        public ActionResult Index(int? page, string searchString)
        {
            var khachhang = db.KhachHangs.Select(s => s);
            //loc theo tên danh mục
            if (!String.IsNullOrEmpty(searchString))
            {
                khachhang = khachhang.Where(p => p.HoTen.Contains(searchString));
            }

            khachhang = khachhang.OrderBy(s => s.ID);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(khachhang.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoTen,Email,DiaChi,SoDT,TenDangNhap,MatKhau,Avatar,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] KhachHang khachHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    khachHang.Avatar = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string upLoadPath = Server.MapPath("~/wwwroot/" + FileName);
                        f.SaveAs(upLoadPath);
                        khachHang.Avatar = FileName;
                    }
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index");


                }

                return View(khachHang);


            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(khachHang);
            }
        }

        // GET: Admin/KhachHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoTen,Email,DiaChi,SoDT,TenDangNhap,MatKhau,Avatar,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] KhachHang khachHang)
        {
            try
            {
               
                    var kh = db.KhachHangs.SingleOrDefault(x => x.ID == khachHang.ID);
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string upLoadPath = Server.MapPath("~/wwwroot/" + FileName);
                        f.SaveAs(upLoadPath);
                        kh.Avatar = FileName;
                    }
                    kh.HoTen = khachHang.HoTen;
                    kh.Email = khachHang.Email;
                    kh.DiaChi = khachHang.DiaChi;
                    kh.SoDT = khachHang.SoDT;
                    kh.TenDangNhap = khachHang.TenDangNhap;
                    kh.MatKhau = khachHang.MatKhau;
                    kh.CreatedDate = khachHang.CreatedDate;
                    kh.CreatedBy = khachHang.CreatedBy;
                    kh.ModifiedDate = khachHang.ModifiedDate;
                    kh.ModifiedBy = khachHang.ModifiedBy;
                    db.SaveChanges();

               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(khachHang);
            }


        }

        // GET: Admin/KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            try
            {
                db.KhachHangs.Remove(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không được xóa bản ghi này!";
                return View("Delete", khachHang);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
