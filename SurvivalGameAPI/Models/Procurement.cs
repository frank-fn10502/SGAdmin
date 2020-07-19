namespace SurvivalGameAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Procurement")]
    public partial class Procurement
    {
        [StringLength(10)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string ProductID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime PurchasingDay { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal UintPrice { get; set; }

        public virtual Products Products { get; set; }
    }
}
