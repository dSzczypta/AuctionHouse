using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IObjects _objects;

        public IndexModel()
        {
            _objects = new Objects();
        }

        public IList<AhObjectToSell> AhObjectToSell { get; set; }

        public async Task OnGetAsync()
        {
            AhObjectToSell = await _objects.GetObjects();
        }



    }
}
