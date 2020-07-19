using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel.Member
{
    public class RegisterViewModel
    {
        public string Name { get; set; }

        //[Required]
        //[RegularExpression(@"^[a-zA-Z_][\w*\u2E80-\u9FFF]*$")]
        //[StringLength(25, MinimumLength = 2, ErrorMessage = "開頭只能為大寫或小寫英文")]
        //public string Account { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string CheckPassword { get; set; }

        public DateTime Birth { get; set; }

        public string PostCode { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}