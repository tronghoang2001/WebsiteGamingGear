namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucTinTuc")]
    public partial class DanhMucTinTuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMucTinTuc()
        {
            TheLoaiTinTucs = new HashSet<TheLoaiTinTuc>();
            TinTucs = new HashSet<TinTuc>();
        }

        [Key]
        public int idDanhMucTT { get; set; }

        [Required]
        [StringLength(50)]
        public string tenDanhMuc { get; set; }

        [StringLength(50)]
        public string icon { get; set; }

        [StringLength(200)]
        public string hinhAnh1 { get; set; }

        [StringLength(200)]
        public string hinhAnh2 { get; set; }

        [StringLength(200)]
        public string moTa { get; set; }

        [StringLength(150)]
        public string link { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TheLoaiTinTuc> TheLoaiTinTucs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
