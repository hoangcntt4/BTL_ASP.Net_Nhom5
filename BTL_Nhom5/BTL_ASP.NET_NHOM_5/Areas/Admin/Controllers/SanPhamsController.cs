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
    public class SanPhamsController : BaseController
    {
        private TrueMartDb db = new TrueMartDb();

        // GET: Admin/SanPhams
        public ActionResult Index(string sortOrder, string searchString,string currentFilter, int? page)
        {
            // Các biến sắp xếp
            ViewBag.CurrentSort = sortOrder;// lấy biến yêu cầu sắp xếp hiện tại
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SapTheoGia = sortOrder == "gia" ? "gia_desc" : "gia";
            //lấy giá trị của bộ lọc dữ liệu hiện tại
            if(searchString != null)
            {
                page = 1;// trang đầu tiên
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var sanpham = db.SanPhams.Include(s => s.DanhMuc).Include(s => s.KhuyenMai).Include(s => s.ThuongHieu).Select(s => s);
            //loc theo tên hàng
            if (!String.IsNullOrEmpty(searchString))
            {
                sanpham = sanpham.Where(p => p.TenSP.Contains(searchString));
            }
            //sắp xếp
            switch (sortOrder)
            {
                case "ten_desc":
                    sanpham = sanpham.OrderByDescending(s => s.TenSP);
                    break;
                case "gia":
                    sanpham = sanpham.OrderBy(s => s.DonGia);
                    break;
                case "gia_desc":
                    sanpham = sanpham.OrderByDescending(s => s.DonGia);
                    break;
                default:
                    sanpham = sanpham.OrderBy(s => s.TenSP);
                    break;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(sanpham.ToPagedList(pageNumber, pageSize));
        }
       /* public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc).Include(s => s.KhuyenMai).Include(s => s.ThuongHieu);
            return View(sanPhams.ToList());
        }
*/
        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM");
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM");
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaTH,MaDM,TenSP,MetaTitle,MoTa,SoLuong,DonGia,BaoHanh,ChiTiet,HinhAnh,Ncc,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MaKM")] SanPham sanPham)
        {
            try
            {

               
                    sanPham.HinhAnh = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string upLoadPath = Server.MapPath("~/wwwroot/images/" + FileName);
                        f.SaveAs(upLoadPath);
                        sanPham.HinhAnh = FileName;
                    }
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
              
              
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;

                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", sanPham.MaKM);
                ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", sanPham.MaTH);
                return View(sanPham);
            }
           

        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            Session["hinhanh"]=sanPham.HinhAnh;
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
            ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", sanPham.MaKM);
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", sanPham.MaTH);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaTH,MaDM,TenSP,MetaTitle,MoTa,SoLuong,DonGia,BaoHanh,ChiTiet,HinhAnh,Ncc,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,MaKM")] SanPham sanPham)
        {
            try
            {
                //if (ModelState.IsValid)
                //{

                    sanPham.HinhAnh = Session["hinhanh"].ToString();
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string upLoadPath = Server.MapPath("~/wwwroot/images/" + FileName);
                        f.SaveAs(upLoadPath);
                        sanPham.HinhAnh = FileName;
                    }

                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                //}
                //else
                //{
                //    ViewBag.Error = "Lỗi nhập dữ liệu!" ;
                //    ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                //    ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", sanPham.MaKM);
                //    ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", sanPham.MaTH);
                //    return View(sanPham);
                //}
               
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sanPham.MaDM);
                ViewBag.MaKM = new SelectList(db.KhuyenMais, "MaKM", "TenKM", sanPham.MaKM);
                ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", sanPham.MaTH);
                return View(sanPham);
            }
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            try
            {
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không thể xóa bản ghi này!";
                return View("Delete", sanPham);
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
