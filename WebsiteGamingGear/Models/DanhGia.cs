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

        public DateTime? ngayThem { get; set; }

        public int? soSao { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
