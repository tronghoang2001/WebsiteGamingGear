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

        [StringLength(200)]
        public string hinhAnh1 { get; set; }

        [StringLength(200)]
        public string hinhAnh2 { get; set; }

        [StringLength(100)]
        public string moTa { get; set; }

        [StringLength(150)]
        public string link { get; set; }

        public virtual DanhMucTinTuc DanhMucTinTuc { get; set; }

        public virtual DanhMucTinTuc DanhMucTinTuc1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
