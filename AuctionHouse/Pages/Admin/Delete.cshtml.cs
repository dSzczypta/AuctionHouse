using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;

namespace AuctionHouse.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly IAdminPanel _adminPanel;

        public DeleteModel()
        {
            _adminPanel = new AdminPanel();
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _adminPanel.DeletePerson(id);

            return RedirectToPage("./Index");
        }
    }
}
