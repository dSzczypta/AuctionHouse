using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.PaymentMethod
{
    public class IndexModel : PageModel
    {
        private readonly IPaymentMethodManager _paymentMethod;

        public IndexModel()
        {
            _paymentMethod = new PaymentMethodManager();
        }

        public IList<AhPaymentMethod> AhPaymentMethod { get;set; }

        public async Task OnGetAsync()
        {
            AhPaymentMethod = await _paymentMethod.GetPaymentMethod();
        }


    }
}
