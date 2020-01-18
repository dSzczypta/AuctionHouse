using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;

namespace AuctionHouse.Pages.PaymentMethod
{
    public class DetailsModel : PageModel
    {
        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public DetailsModel(AuctionHouseCore.Models.AuctionHouseContext context)
        {
            _context = context;
        }

        public AhPaymentMethod AhPaymentMethod { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhPaymentMethod = await _context.AhPaymentMethod.FirstOrDefaultAsync(m => m.Id == id);

            if (AhPaymentMethod == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
