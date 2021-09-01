using BTL_ASP.NET_NHOM_5.Common;
using BTL_ASP.NET_NHOM_5.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_ASP.NET_NHOM_5.Dao
{
    public class KhachHangDao
    {
        TrueMartDb db = null;
        public KhachHangDao()
        {
            db = new TrueMartDb();
        }

        public long Insert(KhachHang entity)
        {
            db.KhachHangs.Add(entity);
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),//Thấy lỗi chưa
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            return entity.ID;
        }
        public long InsertForFacebook(KhachHang entity)
        {
            var KhachHang = db.KhachHangs.SingleOrDefault(x => x.HoTen == entity.HoTen);
            if (KhachHang == null)
            {
                db.KhachHangs.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return KhachHang.ID;
            }

        }
        public bool Update(KhachHang entity)
        {
            try
            {
                var KhachHang = db.KhachHangs.Find(entity.ID);
                KhachHang.HoTen = entity.HoTen;
                if (!string.IsNullOrEmpty(entity.MatKhau))
                {
                    KhachHang.MatKhau = entity.MatKhau;
                }
                KhachHang.DiaChi = entity.DiaChi;
                KhachHang.Email = entity.Email;
                KhachHang.ModifiedBy = entity.ModifiedBy;
                KhachHang.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

        public IEnumerable<KhachHang> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<KhachHang> model = db.KhachHangs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenDangNhap.Contains(searchString) || x.HoTen.Contains(searchString));
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public KhachHang GetById(string TenDangNhap)
        {
            return db.KhachHangs.SingleOrDefault(x => x.TenDangNhap == TenDangNhap);
        }
        public KhachHang ViewDetail(int id)
        {
            return db.KhachHangs.Find(id);
        }
        public int Login(string TenDangNhap, string MatKhau)
        {
            var result = db.KhachHangs.SingleOrDefault(x => x.TenDangNhap == TenDangNhap);
            if (result == null)
            {
                return 0;
            }
            else
            {
                        {
                            if (result.MatKhau == MatKhau)
                                return 1;
                            else
                                return -2;
                        }
             }
        }

        public bool Delete(int id)
        {
            try
            {
                var KhachHang = db.KhachHangs.Find(id);
                db.KhachHangs.Remove(KhachHang);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool CheckTenDangNhap(string TenDangNhap)
        {
            return db.KhachHangs.Count(x => x.TenDangNhap == TenDangNhap) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.KhachHangs.Count(x => x.Email == email) > 0;
        }
    }
}