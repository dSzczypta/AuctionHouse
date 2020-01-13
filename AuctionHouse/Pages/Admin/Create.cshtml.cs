using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuctionHouseCore.Models;

namespace AuctionHouse.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public CreateModel(AuctionHouseCore.Models.AuctionHouseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AspNetUsers AspNetUsers { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AspNetUsers.Add(AspNetUsers);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}