namespace WebsiteGamingGear.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThaoTac")]
    public partial class ThaoTac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThaoTac()
        {
            Quyens = new HashSet<Quyen>();
        }

        [Key]
        public int idThaoTac { get; set; }

        [Required]
        [StringLength(50)]
        public string tenThaoTac { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quyen> Quyens { get; set; }
    }
}
