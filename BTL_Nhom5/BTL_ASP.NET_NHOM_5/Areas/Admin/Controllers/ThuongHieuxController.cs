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
    public class ThuongHieuxController : BaseController
    {
        private TrueMartDb db = new TrueMartDb();

        // GET: Admin/ThuongHieux
        public ActionResult Index(int? page, string searchString)
        {
            var thuonghieu = db.ThuongHieux.Select(s => s);
            //loc theo tên danh mục
            if (!String.IsNullOrEmpty(searchString))
            {
                thuonghieu = thuonghieu.Where(p => p.TenTH.Contains(searchString));
            }

            thuonghieu = thuonghieu.OrderBy(s => s.MaTH);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(thuonghieu.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/ThuongHieux/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThuongHieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTH,TenTH,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] ThuongHieu thuongHieu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ThuongHieux.Add(thuongHieu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               else
                {
                    return View(thuongHieu);
                }    
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(thuongHieu);
            }
        }

        // GET: Admin/ThuongHieux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTH,TenTH,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] ThuongHieu thuongHieu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(thuongHieu).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }
                else
                    return View(thuongHieu);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(thuongHieu);
            }
        }

        // GET: Admin/ThuongHieux/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            try
            {
                db.ThuongHieux.Remove(thuongHieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không xóa được bản ghi này!";
                return View("Delete", thuongHieu);
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
