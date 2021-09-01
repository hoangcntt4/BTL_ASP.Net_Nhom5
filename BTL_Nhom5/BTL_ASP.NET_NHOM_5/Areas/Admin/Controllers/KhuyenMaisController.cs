using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_ASP.NET_NHOM_5.Models;
using PagedList;

namespace BTL_ASP.NET_NHOM_5.Areas.Admin.Controllers
{
    public class KhuyenMaisController : BaseController
    {
        private TrueMartDb db = new TrueMartDb();

        // GET: Admin/KhuyenMais
        public ActionResult Index(int? page, string searchString)
        {
            var khuyenmai = db.KhuyenMais.Select(s => s);
            //loc theo tên danh mục
            if (!String.IsNullOrEmpty(searchString))
            {
                khuyenmai = khuyenmai.Where(p => p.TenKM.Contains(searchString));
            }

            khuyenmai = khuyenmai.OrderBy(s => s.MaKM);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(khuyenmai.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/KhuyenMais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // GET: Admin/KhuyenMais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKM,TenKM,NgayBatDau,NgayKetThuc,Discount,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] KhuyenMai khuyenMai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.KhuyenMais.Add(khuyenMai);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return View(khuyenMai);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "lỗi nhập dữ liệu!" + ex.Message;
                return View(khuyenMai);
            }
        }

        // GET: Admin/KhuyenMais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: Admin/KhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKM,TenKM,NgayBatDau,NgayKetThuc,Discount,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] KhuyenMai khuyenMai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(khuyenMai).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                    return View(khuyenMai);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(khuyenMai);
            }
        }

        // GET: Admin/KhuyenMais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: Admin/KhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            try
            {
                db.KhuyenMais.Remove(khuyenMai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không được xóa bản ghi này!";
                return View("Delete", khuyenMai);
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
