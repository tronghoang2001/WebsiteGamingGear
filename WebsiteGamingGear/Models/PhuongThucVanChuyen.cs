namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhuongThucVanChuyen")]
    public partial class PhuongThucVanChuyen
    {
        [Key]
        public int idVanChuyen { get; set; }

        [Required]
        [StringLength(50)]
        public string tenVanChuyen { get; set; }

        [Column(TypeName = "money")]
        public decimal giaVanChuyen { get; set; }
    }
}
