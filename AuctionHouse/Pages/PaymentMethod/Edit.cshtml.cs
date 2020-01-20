using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.PaymentMethod
{
    public class EditModel : PageModel
    {
        private readonly IPaymentMethodManager _paymentMethod;

        public EditModel()
        {
            _paymentMethod = new PaymentMethodManager();
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _paymentMethod.EditPayment(AhPaymentMethod);
            }
            catch
            {
                if (!_paymentMethod.AhPaymentMethodExists(AhPaymentMethod.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
