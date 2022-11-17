namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tinh_ThanhPho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tinh_ThanhPho()
        {
            DiaChiDDHs = new HashSet<DiaChiDDH>();
            DiaChiKhachHangs = new HashSet<DiaChiKhachHang>();
            Quan_Huyen = new HashSet<Quan_Huyen>();
        }

        [Key]
        public int idTinhThanh { get; set; }

        [Required]
        [StringLength(100)]
        public string tenTinhThanh { get; set; }

        [Required]
        [StringLength(50)]
        public string kieuTinhThanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaChiDDH> DiaChiDDHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaChiKhachHang> DiaChiKhachHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quan_Huyen> Quan_Huyen { get; set; }
    }
}
