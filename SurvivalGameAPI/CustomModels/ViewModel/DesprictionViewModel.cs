using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel
{
    public class DesprictionViewModel
    {
        [JsonProperty("Attr")]
        public List<string> AttrList { get; set; }

        [JsonProperty("Depiction")]
        public string Description { get; set; }
    }
}