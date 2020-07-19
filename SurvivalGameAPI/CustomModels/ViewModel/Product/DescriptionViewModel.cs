using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel.Product
{
    public class DescriptionViewModel
    {
        public List<string> AttrList { get; set; }

        [JsonProperty("Depiction")]
        public string Desc { get; set; }
    }
}