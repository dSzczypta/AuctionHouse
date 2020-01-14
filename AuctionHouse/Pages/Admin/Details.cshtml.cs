using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionHouseCore.Services;
using AuctionHouseCore.Models;

namespace AuctionHouse.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly IAdminPanel _adminPanel;

        public DetailsModel()
        {
            _adminPanel = new AdminPanel();
        }

        public AhPerson AspNetUsers { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AspNetUsers = await _adminPanel.GetPersonDetails(id); 

            if (AspNetUsers == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
