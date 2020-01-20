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
    public class IndexModel : PageModel
    {
        private readonly IShipmentTypeManager _shipmentTypeManager;

        public IndexModel()
        {
            _shipmentTypeManager = new ShipmentTypeManager();
        }

        public IList<AhShipmentType> AhShipmentType { get;set; }

        public async Task OnGetAsync()
        {
            AhShipmentType = await _shipmentTypeManager.GetShipmentType();
        }
    }
}
