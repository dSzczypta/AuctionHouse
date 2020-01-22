using AuctionHouseCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public class AdminPanel : IAdminPanel
    {
        private readonly AuctionHouseContext _context;
        public AdminPanel()
        {
            _context = new AuctionHouseContext();
        }

        public async Task<List<Person>> GetAllUsers()
        {
            var complete = new List<Person>();
            var person = await _context.AhPerson.Include(x => x.AspNetUser).ToListAsync();
            foreach (var item in person)
            {
                var newPerson = new Person()
                {
                    Id = item.AspNetUser.Id,
                    Address = item.Adress,
                    DateOfBirth = item.DateOfBirth,
                    Email = item.AspNetUser.Email,
                    Name = item.Name,
                    Surname = item.Surname
                };
                complete.Add(newPerson);
            }
            return complete;
        }

        public async Task<AhPerson> GetPersonDetails(string id) =>
            await _context.AhPerson.Include(x => x.AspNetUser).FirstAsync(x => x.AspNetUser.Id == id);

        public async Task DeletePerson(string id)
        {
            var user = await _context.AhPerson.FirstAsync(x => x.AspNetUserId == id);
            if (user != null)
            {
                try
                {
                    _context.AhPerson.Remove(user);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    //log
                }

            }
        }

        public async Task<bool> IsUserExist(string id) =>
            await _context.AhPerson.AnyAsync(e => e.AspNetUserId == id);

        public async Task<bool> EditUser(AhPerson person)
        {
            _context.Attach(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<string> GetTypeOfUser(string username)
        {
            try
            {
                using (var ctx = new AuctionHouseContext())
                {
                    var user = await ctx.AspNetUsers.FirstAsync(x => x.Email == username);
                    return ctx.AspNetUserRoles.First(x => x.UserId == user.Id).RoleId;
                }
            }
            catch (Exception)
            {
                return "3";
            }

        }
    }
}
