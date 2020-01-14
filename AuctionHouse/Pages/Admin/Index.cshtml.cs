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
    public class IndexModel : PageModel
    {
        IAdminPanel _adminPanel;
        public IndexModel()
        {
            _adminPanel = new AdminPanel();
        }

        [BindProperty]
        public IList<Person> Users { get;set; }

        public async Task OnGetAsync()
        {
            Users = await _adminPanel.GetAllUsers();
        }
    }
}
