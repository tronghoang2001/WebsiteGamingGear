namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheLoaiTinTuc")]
    public partial class TheLoaiTinTuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TheLoaiTinTuc()
        {
            TinTucs = new HashSet<TinTuc>();
        }

        [Key]
        public int idTheLoaiTT { get; set; }

        public int idDanhMucTT { get; set; }

        [Required]
        [StringLength(50)]
        public string tenTheLoai { get; set; }

        public virtual DanhMucTinTuc DanhMucTinTuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
