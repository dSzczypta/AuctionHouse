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
    public class DeleteModel : PageModel
    {
        private IAuctions _auctions;

        public DeleteModel()
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
                await _auctions.DeleteAuction(AhAuctions);
            }

            return RedirectToPage("./Index");
        }
    }
}
