using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.ShipmentType
{
    public class DetailsModel : PageModel
    {
        private readonly IShipmentTypeManager _shipmentTypeManager;

        public DetailsModel()
        {
            _shipmentTypeManager = new ShipmentTypeManager();
        }

        public AhShipmentType AhShipmentType { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhShipmentType = await _shipmentTypeManager.GetShipmentType(id);

            if (AhShipmentType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
