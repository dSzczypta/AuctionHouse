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

namespace AuctionHouse.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly IAdminPanel _adminPanel;

        public EditModel()
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await _adminPanel.EditUser(AspNetUsers);
            if(!result)
                return NotFound();
           
            return RedirectToPage("./Index");
        }
    }
}
