using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_ASP.NET_NHOM_5.Models
{
    public class ProductViewModel
    {
        
        public long maDm { get; set; }
        public string tenDm { get; set; }
        public string metaTitleDm { get; set; }
        public long? parentID { get; set; }
        public int maSp { get; set; }
        public string tenSp { get; set; }
        public string metaTitleSp { get; set; }
        public  decimal? donGiaSp { get; set; }
        public double? giamGia{ get; set; }
        public string hinhAnh { get; set; }
        public double? giaBan { get; set; }
        public string ChiTiet { get; set; }
    }
}