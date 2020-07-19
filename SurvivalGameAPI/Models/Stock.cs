namespace SurvivalGameAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stock")]
    public partial class Stock
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string ProductID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string TypeID { get; set; }

        public int InvetoryQuantity { get; set; }

        public virtual Products Products { get; set; }
    }
}
