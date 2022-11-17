namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhuongThucThanhToan")]
    public partial class PhuongThucThanhToan
    {
        [Key]
        public int idThanhToan { get; set; }

        [Required]
        [StringLength(100)]
        public string tenThanhToan { get; set; }
    }
}
