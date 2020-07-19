namespace SurvivalGameAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Wishlist")]
    public partial class Wishlist
    {
        [StringLength(10)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string MemberID { get; set; }

        [Required]
        [StringLength(10)]
        public string ProductID { get; set; }

        public virtual Members Members { get; set; }

        public virtual Products Products { get; set; }
    }
}
