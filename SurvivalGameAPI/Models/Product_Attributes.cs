namespace SurvivalGameAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product Attributes")]
    public partial class Product_Attributes
    {
        [StringLength(10)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string PID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Value { get; set; }

        public virtual Products Products { get; set; }
    }
}
