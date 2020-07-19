namespace SurvivalGameAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RelatedProducts
    {
        [StringLength(10)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string ProductID { get; set; }

        [Required]
        [StringLength(10)]
        public string RelationPID { get; set; }

        public virtual Products Products { get; set; }
    }
}
