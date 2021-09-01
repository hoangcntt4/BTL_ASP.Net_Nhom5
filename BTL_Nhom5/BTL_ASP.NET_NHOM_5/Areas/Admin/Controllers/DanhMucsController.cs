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
    public class DanhMucsController : BaseController
    {
        private TrueMartDb db = new TrueMartDb();

        // GET: Admin/DanhMucs
        public ActionResult Index(int? page,string searchString)
        {
            var danhmuc = db.DanhMucs.Select(s => s);
            //loc theo tên danh mục
            if (!String.IsNullOrEmpty(searchString))
            {
                danhmuc = danhmuc.Where(p => p.TenDM.Contains(searchString));
            }
            
            danhmuc = danhmuc.OrderBy(s => s.MaDM);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(danhmuc.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/DanhMucs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // GET: Admin/DanhMucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDM,TenDM,MetaTitle,ParentID,DisplayOrder,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DanhMuc danhMuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.DanhMucs.Add(danhMuc);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(danhMuc);
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(danhMuc);
            }
            
           
        }

        // GET: Admin/DanhMucs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/DanhMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDM,TenDM,MetaTitle,ParentID,DisplayOrder,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DanhMuc danhMuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(danhMuc).State = EntityState.Modified;
                    db.SaveChanges();
                   
                }
                else
                    return View(danhMuc);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(danhMuc);
            }
        }

        // GET: Admin/DanhMucs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/DanhMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            try
            {
                
                db.DanhMucs.Remove(danhMuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không xóa được bản ghi này!";
                return View("Delete",danhMuc);
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
