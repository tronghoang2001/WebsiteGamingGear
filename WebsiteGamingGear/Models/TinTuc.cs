namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TinTuc()
        {
            BinhLuanTinTucs = new HashSet<BinhLuanTinTuc>();
        }

        [Key]
        public int idTinTuc { get; set; }

        public int? idTaiKhoan { get; set; }

        public int? theLoaiTin { get; set; }

        [Required]
        [StringLength(200)]
        public string tieuDeTin { get; set; }

        [StringLength(200)]
        public string tieuDeMoTa { get; set; }

        [Column(TypeName = "ntext")]
        public string noiDung { get; set; }

        public int? luotXem { get; set; }

        [StringLength(200)]
        public string hinhAnh1 { get; set; }

        [StringLength(200)]
        public string hinhAnh2 { get; set; }

        [StringLength(1)]
        public string trangThai { get; set; }

        public DateTime? ngaySoan { get; set; }

        public int? idDanhMucTT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuanTinTuc> BinhLuanTinTucs { get; set; }

        public virtual DanhMucTinTuc DanhMucTinTuc { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual TheLoaiTinTuc TheLoaiTinTuc { get; set; }
    }
}
