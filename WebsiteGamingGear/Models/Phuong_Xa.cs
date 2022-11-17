namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Phuong_Xa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phuong_Xa()
        {
            DiaChiDDHs = new HashSet<DiaChiDDH>();
            DiaChiKhachHangs = new HashSet<DiaChiKhachHang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idPhuongXa { get; set; }

        public int idQuanHuyen { get; set; }

        [Required]
        [StringLength(100)]
        public string tenPhuongXa { get; set; }

        [Required]
        [StringLength(100)]
        public string kieuPhuongXa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaChiDDH> DiaChiDDHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiaChiKhachHang> DiaChiKhachHangs { get; set; }

        public virtual Quan_Huyen Quan_Huyen { get; set; }
    }
}
