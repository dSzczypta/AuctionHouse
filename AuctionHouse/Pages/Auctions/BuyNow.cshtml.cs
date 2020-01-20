using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionHouse.Pages.Auctions
{
    public class BuyNowModel : PageModel
    {

        private readonly IObjects _objects;
        private IAuctions _auctions;
        private IShipmentTypeManager _shipmentTypeManager;
        private IPaymentMethodManager _paymentMethodManager;
        string user;

        public BuyNowModel(IHttpContextAccessor _httpContextAccessor)
        {
            _objects = new Objects();
            _auctions = new AuctionHouseCore.Services.Auctions();
            _shipmentTypeManager = new AuctionHouseCore.Services.ShipmentTypeManager();
            _paymentMethodManager = new AuctionHouseCore.Services.PaymentMethodManager();
            user = _httpContextAccessor.HttpContext.User.Identity.Name;
        }

        [BindProperty]
        public AhObjectToSell AhObjectToSell { get; set; }
        [BindProperty]
        public List<SelectListItem> PaymentMethodList { get; set; }
        [BindProperty]
        public List<SelectListItem> ShipmentTypeList { get; set; }
        [BindProperty]
        public string SelectedPaymentMethod { get; set; }
        [BindProperty]
        public string SelectedShipmentType { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            AhObjectToSell = await _objects.GetObject(id);
            var PaymentMethod = await _paymentMethodManager.GetPaymentMethod();
            PaymentMethodList = PaymentMethod.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            var ShipmentType = await _shipmentTypeManager.GetShipmentType();

            ShipmentTypeList = ShipmentType.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

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

            var auction = new AhAuctions
            {

                ObjectId = AhObjectToSell.Id,
                PaymentMethod = Guid.Parse(SelectedPaymentMethod),
                ShipmentType = Guid.Parse(SelectedShipmentType),
                UserName = user
            };
            string guid = await _auctions.BuyNow(auction);
            return RedirectToPage("./PurchasedItem", new { id = Guid.Parse(guid) });
        }


    }
}