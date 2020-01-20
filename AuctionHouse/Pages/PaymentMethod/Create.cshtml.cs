using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.PaymentMethod
{
    public class CreateModel : PageModel
    {
        private readonly IPaymentMethodManager _paymentMethod;

        public CreateModel()
        {
            _paymentMethod = new PaymentMethodManager();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AhPaymentMethod AhPaymentMethod { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _paymentMethod.AddPaymentMethod(AhPaymentMethod);

            return RedirectToPage("./Index");
        }
    }
}