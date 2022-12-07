namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuanTinTuc")]
    public partial class BinhLuanTinTuc
    {
        [Key]
        public int idBinhLuan { get; set; }

        public int idTaiKhoan { get; set; }

        public int idTinTuc { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string noiDungBL { get; set; }

        [StringLength(1)]
        public string trangThai { get; set; }

        public DateTime? ngayThem { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual TinTuc TinTuc { get; set; }
    }
}
