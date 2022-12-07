namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheLoaiSP")]
    public partial class TheLoaiSP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TheLoaiSP()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public int idTheLoaiSP { get; set; }

        public int idDanhMucSP { get; set; }

        [Required]
        [StringLength(50)]
        public string tenTheLoai { get; set; }

        public virtual DanhMucSP DanhMucSP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
