namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LienHe")]
    public partial class LienHe
    {
        [Key]
        public int idLienHe { get; set; }

        [Required]
        [StringLength(50)]
        public string tenNguoiLH { get; set; }

        [StringLength(10)]
        public string soDienThoai { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string noiDung { get; set; }

        [StringLength(50)]
        public string hinhAnh { get; set; }

        [Column(TypeName = "ntext")]
        public string phanHoiLH { get; set; }

        [StringLength(1)]
        public string trangThai { get; set; }

        public int? idTaiKhoan { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
