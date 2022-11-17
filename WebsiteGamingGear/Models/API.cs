namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("API")]
    public partial class API
    {
        [Key]
        public int idAPI { get; set; }

        [StringLength(500)]
        public string clientID { get; set; }

        [StringLength(500)]
        public string secretKey { get; set; }

        [StringLength(200)]
        public string returnURL { get; set; }

        public bool trangThai { get; set; }

        [StringLength(100)]
        public string tenAPI { get; set; }

        [Column(TypeName = "ntext")]
        public string moTa { get; set; }
    }
}
