using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuctionHouse.Pages.History
{
    public class HIstoryModel : PageModel
    {
        string user;

        public HIstoryModel(IHttpContextAccessor _httpContextAccessor) {
            user = _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        [BindProperty]
        public IList<AhAuctions> AhAuctions { get; set; }

        public async Task OnGetAsync()
        {
            AhAuctions = await Person.GetHistory(user);
        }
    }
}