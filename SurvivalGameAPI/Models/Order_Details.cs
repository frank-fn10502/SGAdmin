namespace SurvivalGameAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order Details")]
    public partial class Order_Details
    {
        [StringLength(10)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string OrderID { get; set; }

        [Required]
        [StringLength(10)]
        public string ProductID { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Products Products { get; set; }
    }
}
