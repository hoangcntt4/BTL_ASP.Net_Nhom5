using BTL_ASP.NET_NHOM_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_ASP.NET_NHOM_5.Dao
{
    public class SanPhamDao
    {
        TrueMartDb db = null;
        public SanPhamDao()
        {
            db = new TrueMartDb();
        }

        public List<SanPham> ListNewSanPham(int top)
        {
            return db.SanPhams.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<string> ListName(string keyword)
        {
            return db.SanPhams.Where(x => x.TenSP.Contains(keyword)).Select(x => x.TenSP).ToList();
        }

        /// <summary>
        /// Get list SanPham by category
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        //public List<SanPhamViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{
        //    totalRecord = db.SanPhams.Where(x => x.CategoryID == categoryID).Count();
        //    var model = (from a in db.SanPhams
        //                 join b in db.SanPhamCategories
        //                 on a.CategoryID equals b.ID
        //                 where a.CategoryID == categoryID
        //                 select new
        //                 {
        //                     CateMetaTitle = b.MetaTitle,
        //                     CateName = b.Name,
        //                     CreatedDate = a.CreatedDate,
        //                     ID = a.ID,
        //                     Images = a.Image,
        //                     Name = a.Name,
        //                     MetaTitle = a.MetaTitle,
        //                     Price = a.Price
        //                 }).AsEnumerable().Select(x => new SanPhamViewModel()
        //                 {
        //                     CateMetaTitle = x.MetaTitle,
        //                     CateName = x.Name,
        //                     CreatedDate = x.CreatedDate,
        //                     ID = x.ID,
        //                     Images = x.Images,
        //                     Name = x.Name,
        //                     MetaTitle = x.MetaTitle,
        //                     Price = x.Price
        //                 });
        //    model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    return model.ToList();
        //}
        //public List<SanPhamViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        //{
        //    totalRecord = db.SanPhams.Where(x => x.Name == keyword).Count();
        //    var model = (from a in db.SanPhams
        //                 join b in db.SanPhamCategories
        //                 on a.CategoryID equals b.ID
        //                 where a.Name.Contains(keyword)
        //                 select new
        //                 {
        //                     CateMetaTitle = b.MetaTitle,
        //                     CateName = b.Name,
        //                     CreatedDate = a.CreatedDate,
        //                     ID = a.ID,
        //                     Images = a.Image,
        //                     Name = a.Name,
        //                     MetaTitle = a.MetaTitle,
        //                     Price = a.Price
        //                 }).AsEnumerable().Select(x => new SanPhamViewModel()
        //                 {
        //                     CateMetaTitle = x.MetaTitle,
        //                     CateName = x.Name,
        //                     CreatedDate = x.CreatedDate,
        //                     ID = x.ID,
        //                     Images = x.Images,
        //                     Name = x.Name,
        //                     MetaTitle = x.MetaTitle,
        //                     Price = x.Price
        //                 });
        //    model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    return model.ToList();
        //}
        /// <summary>
        /// List feature SanPham
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        //public List<SanPham> ListFeatureSanPham(int top)
        //{
        //    return db.SanPhams.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        //}
        //public List<SanPham> ListRelatedSanPhams(long SanPhamId)
        //{
        //    var SanPham = db.SanPhams.Find(SanPhamId);
        //    return db.SanPhams.Where(x => x.ID != SanPhamId && x.CategoryID == SanPham.CategoryID).ToList();
        //}
        public SanPham ViewDetail(long id)
        {
            return db.SanPhams.Find(id);
        }
    }
}