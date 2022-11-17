namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Quan_Huyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quan_Huyen()
        {
            DiaChiDDHs = new HashSet<DiaChiDDH>();
            DiaChiKhachHangs = new HashSet<DiaChiKhachHang>();
            Phuong_Xa = new HashSet<Phuong_Xa>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idQuanHuyen { get; set; }

        public int idTinhThanh { get; set; }

        [Required]
        [StringLength(100)]
        public string tenQuanHuyen { get; set; }

        [Required]
        [StringLength(100)]
        public string kieuQuanHuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaChiDDH> DiaChiDDHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaChiKhachHang> DiaChiKhachHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phuong_Xa> Phuong_Xa { get; set; }

        public virtual Tinh_ThanhPho Tinh_ThanhPho { get; set; }
    }
}
