namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiaChiDDH")]
    public partial class DiaChiDDH
    {
        [Key]
        public int idDCDDH { get; set; }

        [StringLength(10)]
        public string soDienThoai { get; set; }

        [StringLength(50)]
        public string tenKhachHang { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [Column(TypeName = "ntext")]
        public string noiDung { get; set; }

        public int? thanhPho { get; set; }

        public int? quanHuyen { get; set; }

        public int? phuongXa { get; set; }

        public virtual Phuong_Xa Phuong_Xa { get; set; }

        public virtual Quan_Huyen Quan_Huyen { get; set; }

        public virtual Tinh_ThanhPho Tinh_ThanhPho { get; set; }
    }
}
