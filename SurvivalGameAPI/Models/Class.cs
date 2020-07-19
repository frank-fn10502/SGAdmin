namespace SurvivalGameAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Class")]
    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            Products = new HashSet<Products>();
        }

        [StringLength(10)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string CatagoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual Catagory Catagory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
