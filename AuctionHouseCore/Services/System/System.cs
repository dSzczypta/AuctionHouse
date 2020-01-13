using AuctionHouseCore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services.System
{
    class System : ISystem
    {
        private readonly AuctionHouseContext _context;
        public System()
        {
            _context = new AuctionHouseContext();
        }

        public async Task<List<AspNetUsers>> GetAllUsers()
        {

            //var users = _context.AspNetUsers.Select(x => new { x.Email, x.EmailConfirmed, x.UserName }).ToList();
            var users = _context.AspNetUsers.ToList();

            return users;
        }


    }
}
