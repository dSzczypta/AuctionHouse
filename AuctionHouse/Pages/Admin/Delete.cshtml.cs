using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuctionHouseCore.Models;

namespace AuctionHouse.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly AuctionHouseCore.Models.AuctionHouseContext _context;

        public DeleteModel(AuctionHouseCore.Models.AuctionHouseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AspNetUsers AspNetUsers { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetUsers = await _context.AspNetUsers.FirstOrDefaultAsync(m => m.Id == id);

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

            AspNetUsers = await _context.AspNetUsers.FindAsync(id);

            if (AspNetUsers != null)
            {
                _context.AspNetUsers.Remove(AspNetUsers);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
