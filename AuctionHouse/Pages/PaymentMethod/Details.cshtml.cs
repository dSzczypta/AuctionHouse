using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.PaymentMethod
{
    public class DetailsModel : PageModel
    {
        private readonly IPaymentMethodManager _paymentMethod;

        public DetailsModel()
        {
            _paymentMethod = new PaymentMethodManager();
        }

        public AhPaymentMethod AhPaymentMethod { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AhPaymentMethod = await _paymentMethod.GetPaymentMethod(id);

            if (AhPaymentMethod == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
