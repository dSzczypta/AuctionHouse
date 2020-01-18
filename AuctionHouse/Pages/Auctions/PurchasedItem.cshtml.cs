using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouseCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.Pages.Auctions
{
    public class PurchasedItemModel : PageModel
    {
        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public PurchasedItemModel(AuctionHouseCore.Models.AuctionHouseContext context)
        {
            _context = context;
        }

        public AhAuctions AhAuctions { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhAuctions = await _context.AhAuctions
                .Include(a => a.ObjectNavigation)
                .Include(a => a.PaymentMethodNavigation)
                .Include(a => a.ShipmentTypeNavigation).FirstOrDefaultAsync(m => m.Id == id);

            if (AhAuctions == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}