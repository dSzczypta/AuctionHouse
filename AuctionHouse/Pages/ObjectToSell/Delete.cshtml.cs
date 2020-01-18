using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.ObjectToSell
{
    public class DeleteModel : PageModel
    {
        private readonly IObjects _objects;

        public DeleteModel()
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhObjectToSell = await _objects.GetObject(id);

            if (AhObjectToSell != null)
                await _objects.DeleteObject(AhObjectToSell);
            return RedirectToPage("../Index");
        }
    }
}
