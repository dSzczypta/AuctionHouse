﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;
using Microsoft.AspNetCore.Http;

namespace AuctionHouse.Pages.ObjectToSell
{
    public class CreateModel : PageModel
    {
        private readonly IObjects _objectToSell;

        string User;

        public CreateModel(IHttpContextAccessor _httpContextAccessor)
        {
            User = _httpContextAccessor.HttpContext.User.Identity.Name;
            _objectToSell = new Objects();
        } 

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AhObjectToSell AhObjectToSell { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _objectToSell.AddNewObject(AhObjectToSell, User);
            return RedirectToPage("../Index");
        }
    }
}