﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.Auctions
{
    public class DetailsModel : PageModel
    {
        private readonly IAuctions _auctions;

        public DetailsModel()
        {
            _auctions = new AuctionHouseCore.Services.Auctions();
        }

        public AhAuctions AhAuctions { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhAuctions = await _auctions.GetAuctions(id);
          
            if (AhAuctions == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
