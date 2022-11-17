namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuongTrinhGiamGia")]
    public partial class ChuongTrinhGiamGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongTrinhGiamGia()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        public int idGiamGia { get; set; }

        [Required]
        [StringLength(100)]
        public string tenGiamGia { get; set; }

        public DateTime ngayBatDau { get; set; }

        public DateTime ngayKetThuc { get; set; }

        public int giaGiam { get; set; }

        [StringLength(10)]
        public string maGiamGia { get; set; }

        [StringLength(1)]
        public string trangThai { get; set; }

        public int? soLuong { get; set; }

        public int? loaiHinh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
