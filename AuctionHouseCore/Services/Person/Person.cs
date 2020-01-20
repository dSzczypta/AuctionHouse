using AuctionHouseCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public class Person : IPerson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public AuctionHouseContext _context;
        public Person()
        {
            _context = new AuctionHouseContext();
        }

        public static async Task CreateNewPerson(string email, AhPerson person)
        {
            using (var ctx = new AuctionHouseContext())
            {
                try
                {
                    var aspNetUserID = ctx.AspNetUsers.First(x => x.Email == email).Id;
                    person.AspNetUserId = aspNetUserID;
                    ctx.AhPerson.Add(person);
                    await ctx.SaveChangesAsync();
                }
                catch (Exception)
                {
                    //logowanie błędów;
                    throw;
                }

            }
        }

        public static async Task<IList<AhAuctions>> GetHistory(string user)
        {
            using (var ctx = new AuctionHouseContext())
            {
                try
                {
                    var auctions = await ctx.AhAuctions
                        .Include(a => a.ObjectNavigation)
                        .Include(a => a.PaymentMethodNavigation)
                        .Include(a => a.ShipmentTypeNavigation)
                        .Where(x => x.UserName == user).ToListAsync();
                    return auctions;
                }
                catch (Exception)
                {
                    //logowanie błędów;
                    throw;
                }
            }
        }
    }
}
