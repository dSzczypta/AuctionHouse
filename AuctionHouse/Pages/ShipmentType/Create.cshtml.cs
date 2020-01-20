using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.ShipmentType
{
    public class CreateModel : PageModel
    {
        private readonly IShipmentTypeManager _shipmentTypeManager;

        public CreateModel()
        {
            _shipmentTypeManager = new ShipmentTypeManager();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AhShipmentType AhShipmentType { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _shipmentTypeManager.CreateShipmentType(AhShipmentType);

            return RedirectToPage("./Index");
        }
    }
}