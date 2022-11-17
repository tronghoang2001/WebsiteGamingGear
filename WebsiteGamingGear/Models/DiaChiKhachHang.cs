namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiaChiKhachHang")]
    public partial class DiaChiKhachHang
    {
        [Key]
        public int idDiaChiKH { get; set; }

        public int idTaiKhoan { get; set; }

        public int idTinhThanh { get; set; }

        public int idQuanHuyen { get; set; }

        public int idPhuongXa { get; set; }

        public virtual Phuong_Xa Phuong_Xa { get; set; }

        public virtual Quan_Huyen Quan_Huyen { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual Tinh_ThanhPho Tinh_ThanhPho { get; set; }
    }
}
