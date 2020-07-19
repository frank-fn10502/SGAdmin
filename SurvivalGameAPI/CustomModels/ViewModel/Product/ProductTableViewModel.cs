using SurvivalGameAPI.CustomModels.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel.Product
{
    public class ProductTableViewModel
    {
        public List<string> FiledList { get; set; }
        public IEnumerable<ProductViewModel> SvmList { get; set; }
    }
}