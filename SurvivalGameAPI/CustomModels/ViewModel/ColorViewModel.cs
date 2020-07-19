using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel
{
    public class ColorViewModel
    {
        [JsonProperty("color")]
        public string Name { get; set; }

        [JsonProperty("cc")]
        public string ColorCode { get; set; }
    }
}