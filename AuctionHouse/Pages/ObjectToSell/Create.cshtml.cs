using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuctionHouseCore.Models;
using AuctionHouseCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AuctionHouse.Pages.ObjectToSell
{
    public class CreateModel : PageModel
    {
        private readonly IObjects _objectToSell;
        private string user;
        private IHostingEnvironment _environment;

        public CreateModel(IHttpContextAccessor _httpContextAccessor, IHostingEnvironment environment)
        {
            user = _httpContextAccessor.HttpContext.User.Identity.Name;
            _objectToSell = new Objects();
            _environment = environment;
        } 

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AhObjectToSell AhObjectToSell { get; set; }
        [BindProperty]
        public IFormFile Image { set; get; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!Image.FileName.Contains(".jpg"))
                return Page();

            var id = await _objectToSell.AddNewObject(AhObjectToSell, user);

            var path = Path.Combine(_environment.ContentRootPath, @"wwwroot\uploads", id + ".jpg");
            await _objectToSell.SaveImage(Image, path);

            return RedirectToPage("../Index");
        }
    }
}