namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            BinhLuanTinTucs = new HashSet<BinhLuanTinTuc>();
            DanhGias = new HashSet<DanhGia>();
            DonDatHangs = new HashSet<DonDatHang>();
            LienHes = new HashSet<LienHe>();
            TinTucs = new HashSet<TinTuc>();
        }

        public int id { get; set; }

        [Column("taiKhoan")]
        [Required]
        [StringLength(50)]
        public string taiKhoan1 { get; set; }

        [Required]
        [StringLength(200)]
        public string matKhau { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string requestCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ten { get; set; }

        [Required]
        [StringLength(10)]
        public string soDienThoai { get; set; }

        [StringLength(200)]
        public string anhDaiDien { get; set; }

        public DateTime? ngaySinh { get; set; }

        [StringLength(1)]
        public string gioiTinh { get; set; }

        [StringLength(1)]
        public string trangThai { get; set; }

        public int? quyen { get; set; }

        [StringLength(200)]
        public string diaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuanTinTuc> BinhLuanTinTucs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LienHe> LienHes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
