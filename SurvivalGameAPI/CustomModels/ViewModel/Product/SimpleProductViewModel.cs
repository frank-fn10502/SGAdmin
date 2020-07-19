using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel.Product
{
    public class SimpleProductViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public OnSale onSale { get; set; }
        public IEnumerable<string> ImgList { get; set; }
    }
}