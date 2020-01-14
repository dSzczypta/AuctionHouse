using AuctionHouseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

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
                catch (Exception e)
                {
                    //logowanie błędów;
                }
                
            }
        }
    }
}
