using BTL_ASP.NET_NHOM_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_ASP.NET_NHOM_5.Dao
{
    public class QuanTriDao
    {
        TrueMartDb db = null;
        public QuanTriDao()
        {
            db = new TrueMartDb();
        }

        public long Insert(QuanTri entity)
        {
            db.QuanTris.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public long InsertForFacebook(QuanTri entity)
        {
            var QuanTri = db.QuanTris.SingleOrDefault(x => x.HoTen == entity.HoTen);
            if (QuanTri == null)
            {
                db.QuanTris.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return QuanTri.ID;
            }

        }
        public bool Update(QuanTri entity)
        {
            try
            {
                var QuanTri = db.QuanTris.Find(entity.ID);
                QuanTri.HoTen = entity.HoTen;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    QuanTri.Password = entity.Password;
                }
                QuanTri.DiaChi = entity.DiaChi;
               
             
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }

       

        public QuanTri GetById(string TenDangNhap)
        {
            return db.QuanTris.SingleOrDefault(x => x.UserName == TenDangNhap);
        }
        public QuanTri ViewDetail(int id)
        {
            return db.QuanTris.Find(id);
        }
        public int Login(string TenDangNhap, string Password)
        {
            var result = db.QuanTris.SingleOrDefault(x => x.UserName == TenDangNhap);
            if (result == null)
            {
                return 0;
            }
            else
            {
                {
                    if (result.Password == Password)
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
                var QuanTri = db.QuanTris.Find(id);
                db.QuanTris.Remove(QuanTri);
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
            return db.QuanTris.Count(x => x.UserName == TenDangNhap) > 0;
        }
       
    }
}