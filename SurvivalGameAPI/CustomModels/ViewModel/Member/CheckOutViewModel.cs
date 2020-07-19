using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel.Member
{
    public class CheckOutViewModel
    {
        public string Name { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? Total { get; set; }
    }
}