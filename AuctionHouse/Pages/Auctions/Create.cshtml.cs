using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuctionHouseCore.Models;

namespace AuctionHouse.Pages.Auctions
{
    public class CreateModel : PageModel
    {
        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public CreateModel(AuctionHouseCore.Models.AuctionHouseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ObjectId"] = new SelectList(_context.AhObjectToSell, "Id", "Id");
        ViewData["PaymentMethod"] = new SelectList(_context.AhPaymentMethod, "Id", "Name");
        ViewData["ShipmentType"] = new SelectList(_context.AhShipmentType, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public AhAuctions AhAuctions { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AhAuctions.Add(AhAuctions);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}