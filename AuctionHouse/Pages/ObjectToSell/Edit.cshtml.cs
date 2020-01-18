using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.ObjectToSell
{
    public class EditModel : PageModel
    {
        private readonly IObjects _objects;

        public EditModel()
        {
            _objects = new Objects();
        }

        [BindProperty]
        public AhObjectToSell AhObjectToSell { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhObjectToSell = await _objects.GetObject(id);

            if (AhObjectToSell == null)
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

            var result = await _objects.EditObject(AhObjectToSell);
            
            if(!result)
                return NotFound();

            return RedirectToPage("./Index");
        }
    }
}
