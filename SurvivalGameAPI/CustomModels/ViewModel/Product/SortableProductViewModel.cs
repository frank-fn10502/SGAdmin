using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel.Product
{
    public class SortableProductViewModel
    {
        public string ID { get; set; }
        public string ClassID { get; set; }
        public string CatagoryID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Depiction { get; set; }
        public string ManufacturerName { get; set; }
        public decimal? Price { get; set; }
        public int? InvetoryQuantity { get; set; }
        public DateTime? PurchasingDay { get; set; }
        public int OrderAmount { get; set; }
        public IEnumerable<string> ImgList { get; set; }
    }
}