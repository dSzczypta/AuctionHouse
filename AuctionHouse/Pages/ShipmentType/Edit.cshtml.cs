using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.ShipmentType
{
    public class EditModel : PageModel
    {
        private readonly IShipmentTypeManager _shipmentTypeManager;
        public EditModel()
        {
            _shipmentTypeManager = new ShipmentTypeManager();
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _shipmentTypeManager.EditShipmentType(AhShipmentType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! _shipmentTypeManager.AhShipmentTypeExists(AhShipmentType.Id))
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
    }
}
