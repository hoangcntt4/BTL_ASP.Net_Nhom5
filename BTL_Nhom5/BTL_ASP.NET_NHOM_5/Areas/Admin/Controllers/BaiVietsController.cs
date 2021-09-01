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
    public class BaiVietsController : BaseController
    {
        private TrueMartDb db = new TrueMartDb();

        // GET: Admin/BaiViets
        public ActionResult Index(int? page)
        {
            var baiviet = db.BaiViets.Select(s => s);
            baiviet = baiviet.OrderBy(s => s.MaBV);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(baiviet.ToPagedList(pageNumber,pageSize));
        }

        // GET: Admin/BaiViets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // GET: Admin/BaiViets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BaiViets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBV,TieuDe,NoiDung,HinhAnh,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] BaiViet baiViet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    baiViet.HinhAnh = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string upLoadPath = Server.MapPath("~/wwwroot/" + FileName);
                        f.SaveAs(upLoadPath);
                        baiViet.HinhAnh = FileName;
                    }
                    db.BaiViets.Add(baiViet);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(baiViet);
                }    
               
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(baiViet);
            }
           


        }

        // GET: Admin/BaiViets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            Session["hinhanh"] = baiViet.HinhAnh;
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // POST: Admin/BaiViets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBV,TieuDe,NoiDung,HinhAnh,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] BaiViet baiViet)
        {
            try
            {
               

                   baiViet.HinhAnh = Session["hinhanh"].ToString();
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string upLoadPath = Server.MapPath("~/wwwroot/" + FileName);
                        f.SaveAs(upLoadPath);
                        baiViet.HinhAnh = FileName;
                    }
                    db.Entry(baiViet).State = EntityState.Modified;
                    db.SaveChanges();
                   
               
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(baiViet);
            }
        }

        // GET: Admin/BaiViets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViets.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // POST: Admin/BaiViets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiViet baiViet = db.BaiViets.Find(id);
            db.BaiViets.Remove(baiViet);
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
    }
}
