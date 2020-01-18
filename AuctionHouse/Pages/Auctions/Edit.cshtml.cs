using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;

namespace AuctionHouse.Pages.Auctions
{
    public class EditModel : PageModel
    {
        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public EditModel(AuctionHouseCore.Models.AuctionHouseContext context)
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
           ViewData["ObjectId"] = new SelectList(_context.AhObjectToSell, "Id", "AddedBy");
           ViewData["PaymentMethod"] = new SelectList(_context.AhPaymentMethod, "Id", "Name");
           ViewData["ShipmentType"] = new SelectList(_context.AhShipmentType, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AhAuctions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AhAuctionsExists(AhAuctions.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AhAuctionsExists(Guid id)
        {
            return _context.AhAuctions.Any(e => e.Id == id);
        }
    }
}
