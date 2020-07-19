using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.DTOModel
{
    public class NewestProductDTO
    {
        public string ID { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Depiction { get; set; }
        public string ManufacturerName { get; set; }
        public decimal? Price { get; set; }
        public int? InvetoryQuantity { get; set; }
        public DateTime? PurchasingDay { get; set; }
    }
}