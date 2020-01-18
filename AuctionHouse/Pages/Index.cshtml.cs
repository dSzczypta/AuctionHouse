﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouseCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.Pages
{
    public class IndexModel : PageModel
    {

        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public IndexModel(AuctionHouseCore.Models.AuctionHouseContext context)
        {
            _context = context;
        }

        public IList<AhObjectToSell> AhObjectToSell { get; set; }

        public async Task OnGetAsync()
        {
            AhObjectToSell = await _context.AhObjectToSell.ToListAsync();
        }



    }
}
