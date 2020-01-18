using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.ObjectToSell
{
    public class DetailsModel : PageModel
    {
        private readonly IObjects _objects;

        public DetailsModel()
        {
            _objects = new Objects();
        }

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
    }
}
