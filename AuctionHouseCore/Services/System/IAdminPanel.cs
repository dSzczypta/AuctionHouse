using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public interface IAdminPanel
    {
        Task<List<Person>> GetAllUsers();
    }
}
