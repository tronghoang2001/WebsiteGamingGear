namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDatHang")]
    public partial class DonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDatHang()
        {
            ChiTietDDHs = new HashSet<ChiTietDDH>();
        }

        [Key]
        public int idDDH { get; set; }

        public DateTime ngayDat { get; set; }

        public int idTaiKhoan { get; set; }

        [StringLength(1)]
        public string trangThaiThanhToan { get; set; }

        [StringLength(1)]
        public string trangThai { get; set; }

        [Column(TypeName = "ntext")]
        public string ghiChu { get; set; }

        public int tongTien { get; set; }

        [StringLength(100)]
        public string tenNguoiNhan { get; set; }

        [StringLength(200)]
        public string diaChiNguoiNhan { get; set; }

        [StringLength(10)]
        public string sdtNguoiNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDDH> ChiTietDDHs { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
