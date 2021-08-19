namespace BTL_ASP.NET_NHOM_5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        public int MaSP { get; set; }

        public int MaTH { get; set; }

        public long MaDM { get; set; }

        [StringLength(250)]
        public string TenSP { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string MoTa { get; set; }

        public int? SoLuong { get; set; }

        public decimal? DonGia { get; set; }

        public int? BaoHanh { get; set; }

        [Column(TypeName = "ntext")]
        public string ChiTiet { get; set; }

        [StringLength(250)]
        public string HinhAnh { get; set; }

        [StringLength(100)]
        public string Ncc { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public int MaKM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual KhuyenMai KhuyenMai { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }
    }
}
