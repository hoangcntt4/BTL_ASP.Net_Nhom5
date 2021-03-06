//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTL_ASP.NET_NHOM_5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }
    
        public int MaSP { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]
        [DisplayName("Mã thương hiệu")]
        public int MaTH { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]
        [DisplayName("Mã danh mục")]
        public long MaDM { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]
        [DisplayName("Tên sản phẩm")]
        public string TenSP { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]

        public string MetaTitle { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]
        [DisplayName("Mô tả")]
        public string MoTa { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]
        [DisplayName("Số lượng")]
        public Nullable<int> SoLuong { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]
        [DisplayName("Đơn giá")]
        public Nullable<decimal> DonGia { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]
        [DisplayName("Bảo hành")]
        public Nullable<int> BaoHanh { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]
        [DisplayName("Chi tiết")]
        public string ChiTiet { get; set; }
        [Required(ErrorMessage = "Bạn không được bỏ trống trường này")]
        [DisplayName("Hình ảnh")]
        public string HinhAnh { get; set; }
        [DisplayName("Nhà cung cấp")]
        public string Ncc { get; set; }
        [DisplayName("Ngày tạo")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DisplayName("Người tạo")]
        public string CreatedBy { get; set; }
        [DisplayName("Ngày sửa")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [DisplayName("Người sửa")]
        public string ModifiedBy { get; set; }
        [DisplayName("Mã khuyến mại")]
        public int MaKM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }
        public virtual KhuyenMai KhuyenMai { get; set; }
        public virtual ThuongHieu ThuongHieu { get; set; }
    }
}
