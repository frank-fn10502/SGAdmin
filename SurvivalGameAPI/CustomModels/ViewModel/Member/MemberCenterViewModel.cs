using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurvivalGameAPI.CustomModels.ViewModel.Member
{
    public class MemberCenterViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PostCode { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }

        // History 內容
        public IEnumerable<HistoryItems> HistoryItemList { get; set; }

        // Wishlist 內容
        public IEnumerable<WishlistItems> WishlistItemsList { get; set; }
    }

    public class WishlistItems
    {
        public string WishlistImg { get; set; }
        public string WishlistName { get; set; }
        public decimal? WishlistPrice { get; set; }
    }

    public class HistoryItems
    {
        public string HistoryImg { get; set; }
        public string HistoryName { get; set; }
        public decimal? HistoryPrice { get; set; }
        public int? HistoryQuantity { get; set; }
    }
}