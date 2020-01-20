using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.Auctions
{
    public class IndexModel : PageModel
    {
        private IAuctions _auctions;

        public IndexModel()
        {
            _auctions = new AuctionHouseCore.Services.Auctions();
        }

        [BindProperty]
        public IList<AhAuctions> AhAuctions { get;set; }
        

        public async Task OnGetAsync()
        {
            AhAuctions = await _auctions.GetAuctions();
        }
    }
}
