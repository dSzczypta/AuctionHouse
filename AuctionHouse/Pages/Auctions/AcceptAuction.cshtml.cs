using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuctionHouse.Pages.Auctions
{
    public class AcceptAuctionModel : PageModel
    {
        private IAuctions _auctions;
        public AcceptAuctionModel()
        {
            _auctions = new AuctionHouseCore.Services.Auctions();
        }
        [BindProperty]
        public AhAuctions AhAuctions { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhAuctions = await _auctions.GetAuctions(id);

            if (AhAuctions == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhAuctions = await _auctions.GetAuctions(id);

            if (AhAuctions != null)
            {
                await _auctions.AcceptAuction(AhAuctions.Id);
            }

            return RedirectToPage("./Index");


        }
    }
}