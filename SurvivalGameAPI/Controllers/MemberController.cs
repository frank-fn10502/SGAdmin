using SurvivalGameAPI.CustomModels.ViewModel.Member;
using SurvivalGameAPI.ResultModel;
using SurvivalGameAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SurvivalGameAPI.Controllers
{
    public class MemberController : ApiController
    {
        private MemberService _service;
        public MemberController()
        {
            _service = new MemberService();
        }
        
        [HttpPost]
        public APIResult GetCheckOut(string id)
        {
            return new APIResult()
            {
                IsSuccess = true,
                ExceptionString = null,
                Data = _service.GetCheckOut(id)
            };
        }

        [HttpPost]
        public APIResult CheckLogin(LoginViewModel loginVM)
        {
            return _service.CheckLogin(loginVM);
        }

        [HttpPost]
        public APIResult CheckRedister(RegisterViewModel registerVM)
        {
            return _service.CheckRedister(registerVM);
        }

        [HttpPost]
        public APIResult GetMemberCenter(string id)
        {
            return new APIResult()
            {
                IsSuccess = true,
                ExceptionString = null,
                Data = _service.GetMemberCenter(id)
            };
        }
    }
}