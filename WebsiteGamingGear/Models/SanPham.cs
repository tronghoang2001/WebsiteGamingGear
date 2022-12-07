namespace WebsiteGamingGear.Models
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
            ChiTietDDHs = new HashSet<ChiTietDDH>();
            DanhGias = new HashSet<DanhGia>();
        }

        [Key]
        public int idSanPham { get; set; }

        public int idTheLoaiSP { get; set; }

        public int idThuongHieu { get; set; }

        [Required]
        [StringLength(200)]
        public string tenSanPham { get; set; }

        public int gia { get; set; }

        public int? luotXem { get; set; }

        public int? luotMua { get; set; }

        public int? soLuong { get; set; }

        [Required]
        [StringLength(100)]
        public string hinhAnh { get; set; }

        [Column(TypeName = "ntext")]
        public string moTa { get; set; }

        [Column(TypeName = "ntext")]
        public string moTaChiTiet { get; set; }

        public DateTime ngayThem { get; set; }

        [StringLength(100)]
        public string hinhAnh1 { get; set; }

        [StringLength(100)]
        public string hinhAnh2 { get; set; }

        [StringLength(100)]
        public string hinhAnh3 { get; set; }

        [StringLength(100)]
        public string hinhAnh4 { get; set; }

        [StringLength(100)]
        public string hinhAnh5 { get; set; }

        public int? idDanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDDH> ChiTietDDHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        public virtual DanhMucSP DanhMucSP { get; set; }

        public virtual TheLoaiSP TheLoaiSP { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }
    }
}
