using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;

namespace AuctionHouse.Pages.ShipmentType
{
    public class DetailsModel : PageModel
    {
        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public DetailsModel(AuctionHouseCore.Models.AuctionHouseContext context)
        {
            _context = context;
        }

        public AhShipmentType AhShipmentType { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhShipmentType = await _context.AhShipmentType.FirstOrDefaultAsync(m => m.Id == id);

            if (AhShipmentType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
