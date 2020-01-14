using AuctionHouseCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public interface IAdminPanel
    {
        Task<List<Person>> GetAllUsers();
        Task<AhPerson> GetPersonDetails(string id);
        Task DeletePerson(string id);
    }
}
