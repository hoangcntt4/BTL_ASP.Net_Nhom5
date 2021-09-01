using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL_ASP.NET_NHOM_5.Models;
using PagedList;

namespace BTL_ASP.NET_NHOM_5.Controllers
{
    public class HomeController : Controller
    {
        private TrueMartDb db = new TrueMartDb();
        // GET: Home

        public ActionResult Index()
        {
            var query = from dm in db.DanhMucs
                        join sp in db.SanPhams
                        on dm.MaDM equals sp.MaDM
                        select new ProductViewModel()
                        {
                            maDm=dm.MaDM,
                            tenDm=dm.TenDM,
                            metaTitleDm=dm.MetaTitle,
                            parentID=dm.ParentID,
                            maSp=sp.MaSP,
                            tenSp=sp.TenSP,
                            metaTitleSp=sp.MetaTitle,
                            donGiaSp=sp.DonGia,
                            giamGia=sp.KhuyenMai.Discount,
                            hinhAnh=sp.HinhAnh
                        };
            var danhMuc = db.DanhMucs.Where(x => x.ParentID == -1);
            ViewBag.dem = danhMuc.Count();
            return View(query.ToList());
        }
      
      
        public ActionResult ViewProductsByCategory(long cateId, string sortOrder, int? page)//Xem sản phẩm theo danh muc
        {
            var danhMuc = db.DanhMucs.Where(x => x.MaDM == cateId).SingleOrDefault();
            var query = from dm in db.DanhMucs
                        join sp in db.SanPhams
                        on dm.MaDM equals sp.MaDM
                        where (dm.MaDM==cateId || dm.ParentID==cateId)
                        select new ProductViewModel()
                        {
                            maDm = dm.MaDM,
                            tenDm = dm.TenDM,
                            metaTitleDm = dm.MetaTitle,
                            parentID = dm.ParentID,
                            maSp = sp.MaSP,
                            tenSp = sp.TenSP,
                            metaTitleSp = sp.MetaTitle,
                            donGiaSp = sp.DonGia,
                            giamGia = sp.KhuyenMai.Discount,
                            hinhAnh = sp.HinhAnh,
                            
                        };
            ViewBag.prid = danhMuc.ParentID;
            ViewBag.cateid = cateId;
            ViewBag.Name = danhMuc.TenDM;
            ViewBag.Mt = danhMuc.MetaTitle;
            ViewBag.Prcateid = 0;
            ViewBag.prName = "";
            ViewBag.prMt = "";
            if (danhMuc.ParentID != -1)
            {
                var dmpr= db.DanhMucs.Where(x => x.MaDM == danhMuc.ParentID).SingleOrDefault();
                ViewBag.prcateid = dmpr.MaDM;
                ViewBag.prName = dmpr.TenDM;
                ViewBag.prMt = dmpr.MetaTitle;
            }
            ViewBag.CrtSort =sortOrder;
            ViewBag.SapTheoMacDinh = String.IsNullOrEmpty(sortOrder) ? "Mac_Dinh" : "";
            ViewBag.SapTheoGiaTang = sortOrder == "" ? "gia_desc" : "gia";
            ViewBag.SapTheoGiaGiam = sortOrder == "" ? "gia" : "gia_desc";
            ViewBag.SapTheoSale = sortOrder==""?"gia":"sale";
            var products = query;
            switch (sortOrder)
            {
                case "Mac_Dinh":
                    products = products.OrderBy(s => s.tenSp);
                    ViewBag.CurrentSort = "Mặc định";
                    break;
                case "gia":
                    products = products.OrderBy(s => s.donGiaSp);
                    ViewBag.CurrentSort = "Giá tăng dần";
                    break;
                case "gia_desc":
                    products = products.OrderByDescending(s => s.donGiaSp);
                    ViewBag.CurrentSort = "Giá giảm dần";
                    break;
                case "sale":
                    ViewBag.CurrentSort = "Sale";
                    products = products.OrderByDescending(s => s.giamGia);
                    break;
                default:
                    ViewBag.CurrentSort = "Mặc định";
                    products = products.OrderBy(s => s.tenSp);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ViewProductDetails(long productid)//Xem chi tiết sản phẩm
        {
            var query = db.SanPhams.Where(x => x.MaSP == productid).SingleOrDefault();
            ViewBag.cateid = query.MaDM;
            ViewBag.catename = query.DanhMuc.TenDM;
             return View(query);
        }
        public ActionResult ViewTerms()//Xem điều khoản
        {
            var baiviet = db.BaiViets.Where(b => b.MaBV == 1 || b.MaBV == 2 || b.MaBV == 3 || b.MaBV == 4 || b.MaBV == 5 || b.MaBV == 6).ToList();
            return View(baiviet);
        }
        public ActionResult ViewPosts()//Xem bài viết
        {
            return View(db.BaiViets.ToList());
        }
        public ActionResult viewCode()
        {

            return View();
        }
        [HttpPost]
        public ActionResult viewCode(string q)
        {
            ViewBag.ma = q;
            if (q.Length < 3 || q.Length > 45)
            {
                ViewBag.Error = "Độ dài không hợp lệ";
                ViewBag.ma = null;
            }
            
            return View();
        }
        public ActionResult Search(string sortOrder, string q, string currentFilter, int? page)//Tìm sản phẩm
        {
            if (q != null)
            {
                page = 1;
            }
            else
            {
                q = currentFilter;
            }
            ViewBag.CurrentFilter = q;
            var query = from dm in db.DanhMucs
                        join sp in db.SanPhams
                        on dm.MaDM equals sp.MaDM
                        where sp.TenSP.ToUpper().Contains(q.ToUpper())
                        select new ProductViewModel()
                        {
                            maDm = dm.MaDM,
                            tenDm = dm.TenDM,
                            metaTitleDm = dm.MetaTitle,
                            parentID = dm.ParentID,
                            maSp = sp.MaSP,
                            tenSp = sp.TenSP,
                            metaTitleSp = sp.MetaTitle,
                            donGiaSp = sp.DonGia,
                            giamGia = sp.KhuyenMai.Discount,
                            hinhAnh = sp.HinhAnh,

                        };
            ViewBag.sl = query.Count();
            ViewBag.CrtSort = sortOrder;
            ViewBag.SapTheoMacDinh = String.IsNullOrEmpty(sortOrder) ? "Mac_Dinh" : "";
            ViewBag.SapTheoGiaTang = sortOrder == "" ? "gia_desc" : "gia";
            ViewBag.SapTheoGiaGiam = sortOrder == "" ? "gia" : "gia_desc";
            ViewBag.SapTheoSale = sortOrder == "" ? "gia" : "sale";
            ViewBag.CurrentSort = sortOrder;
            var products = query;
            switch (sortOrder)
            {
                case "Mac_Dinh":
                    products = products.OrderBy(s => s.tenSp);
                    ViewBag.CurrentSort = "Mặc định";
                    break;
                case "gia":
                    products = products.OrderBy(s => s.donGiaSp);
                    ViewBag.CurrentSort = "Giá tăng dần";
                    break;
                case "gia_desc":
                    products = products.OrderByDescending(s => s.donGiaSp);
                    ViewBag.CurrentSort = "Giá giảm dần";
                    break;
                case "sale":
                    ViewBag.CurrentSort = "Sale";
                    products = products.OrderByDescending(s => s.giamGia);
                    break;
                default:
                    ViewBag.CurrentSort = "Mặc định";
                    products = products.OrderBy(s => s.tenSp);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult ProductCategory()
        {
            var model = db.DanhMucs;
            return PartialView(model.ToList());
        }
        public PartialViewResult trademarkProduct()
        {
            var model = db.ThuongHieux;
            return PartialView(model.ToList());
        }
    }
}