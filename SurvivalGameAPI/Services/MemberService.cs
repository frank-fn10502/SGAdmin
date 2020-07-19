using SurvivalGameAPI.CustomModels.ViewModel.Member;
using SurvivalGameAPI.Repositories;
using SurvivalGameAPI.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.Services
{
    public class MemberService
    {
        private MemberRepository _repo;
        public MemberService()
        {
            _repo = new MemberRepository();
        }
        public CheckOutViewModel GetCheckOut(string orderID)
        {
            return _repo.GetCheckOut(orderID);
        }
        public APIResult CheckLogin(LoginViewModel loginVM)
        {
            return _repo.CheckLogin(loginVM);
        }

        public APIResult CheckRedister(RegisterViewModel registerVM)
        {
            return _repo.CheckRedister(registerVM);
        }

        public MemberCenterViewModel GetMemberCenter(string memberID)
        {
            return _repo.GetMemberCenter(memberID);
        }
    }
}