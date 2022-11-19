namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhGia")]
    public partial class DanhGia
    {
        [Key]
        public int idDanhGia { get; set; }

        public int idNguoiDanhGia { get; set; }

        public int idSanPham { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string noiDung { get; set; }

        [StringLength(1)]
        public string trangThai { get; set; }

        [StringLength(100)]
        public string tenNguoiDanhGia { get; set; }

        public DateTime? ngayThem { get; set; }

        [StringLength(200)]
        public string anhDaiDien { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
