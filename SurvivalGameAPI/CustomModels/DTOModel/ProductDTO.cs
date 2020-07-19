using SurvivalGameAPI.CustomModels.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.DTOModel
{
    public class ProductDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string CatagoryName { get; set; }
        public string ManufacturerName { get; set; }
        public decimal? Price { get; set; }
        public int? InvetoryQuantity { get; set; }
        public string Color { get; set; }
        public string Depiction { get; set; }
        public string Img { get; set; }
        public string RelationPID { get; set; }
        public string paName { get; set; }
        public string paValue { get; set; }
    }
}