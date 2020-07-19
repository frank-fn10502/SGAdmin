using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.ResultModel
{
    public class APIResult
    {
        public bool IsSuccess { get; set; }
        public string ExceptionString { get; set; }
        public object Data { get; set; }
    }
}