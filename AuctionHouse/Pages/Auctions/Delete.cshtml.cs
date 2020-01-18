using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;

namespace AuctionHouse.Pages.Auctions
{
    public class DeleteModel : PageModel
    {
        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public DeleteModel(AuctionHouseCore.Models.AuctionHouseContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhAuctions = await _context.AhAuctions.FindAsync(id);

            if (AhAuctions != null)
            {
                _context.AhAuctions.Remove(AhAuctions);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
