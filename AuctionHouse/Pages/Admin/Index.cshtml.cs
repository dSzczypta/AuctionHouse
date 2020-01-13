﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;

namespace AuctionHouse.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public IndexModel(AuctionHouseCore.Models.AuctionHouseContext context)
        {
            _context = context;
        }

        public IList<AspNetUsers> AspNetUsers { get;set; }

        public async Task OnGetAsync()
        {
            AspNetUsers = await _context.AspNetUsers.ToListAsync();
        }
    }
}
