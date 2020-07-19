using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel.Product
{
    public class CatagoryViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public IEnumerable<ClassViewModel> SubCatagoryList { get; set; }
    }
    public class ClassViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}