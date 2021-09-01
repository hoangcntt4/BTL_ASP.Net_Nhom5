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
    public class QuanTrisController : BaseController
    {
        private TrueMartDb db = new TrueMartDb();

        // GET: Admin/QuanTris
        public ActionResult Index(int ? page)
        {
            var quantri = db.QuanTris.Select(s => s);
            quantri = quantri.OrderBy(s => s.ID);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(quantri.ToPagedList(pageNumber,pageSize));
        }
        // GET: Admin/QuanTris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // GET: Admin/QuanTris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QuanTris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoTen,DiaChi,SoDT,UserName,Password,Avatar,ChucVu")] QuanTri quanTri)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    quanTri.Avatar = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UpLoadPath = Server.MapPath("~/wwwroot/" + FileName);
                        f.SaveAs(UpLoadPath);
                        quanTri.Avatar = FileName;
                    }
                    var check = db.QuanTris.Where(x => x.UserName == quanTri.UserName).FirstOrDefault();
                    if (check != null)
                    {
                        ViewBag.Error = "Tên đăng nhập đã tồn tại";
                        return View(quanTri);
                    }
                    db.QuanTris.Add(quanTri);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(quanTri);
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(quanTri);
            }
        }

        // GET: Admin/QuanTris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // POST: Admin/QuanTris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoTen,DiaChi,SoDT,UserName,Password,Avatar,ChucVu")] QuanTri quanTri)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var qt = db.QuanTris.SingleOrDefault(x => x.ID == quanTri.ID);
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadFile = Server.MapPath("~/wwwroot/" + FileName);
                        f.SaveAs(UploadFile);
                        qt.Avatar = FileName;
                    }
                    qt.UserName = quanTri.UserName;
                    qt.Password = quanTri.Password;
                    qt.SoDT = quanTri.SoDT;
                    qt.DiaChi = quanTri.DiaChi;
                    qt.ChucVu = quanTri.ChucVu;
                    db.SaveChanges();
                }
                else
                     return View(quanTri);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(quanTri);
            }
            
        }

        // GET: Admin/QuanTris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanTri quanTri = db.QuanTris.Find(id);
            if (quanTri == null)
            {
                return HttpNotFound();
            }
            return View(quanTri);
        }

        // POST: Admin/QuanTris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuanTri quanTri = db.QuanTris.Find(id);
            db.QuanTris.Remove(quanTri);
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
