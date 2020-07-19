using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel.Product
{
    public class ProductViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string CatagoryName { get; set; }
        public string ManufacturerName { get; set; }
        public decimal? Price { get; set; }
        public int? InvetoryQuantity { get; set; }
        public IEnumerable<ColorViewModel> Color { get; set; }
        public string Depiction { get; set; }
        public IEnumerable<string> ImgList { get; set; }
        public IEnumerable<PAttributeViewModel> PAttributeList { get; set; }
        public IEnumerable<PAttributeViewModel> UnSortAttributeList { get; set; }
        public IEnumerable<string> RelatedProductList { get; set; }
    }
}