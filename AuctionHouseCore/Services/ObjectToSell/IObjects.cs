using AuctionHouseCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public interface IObjects
    {
        Task AddNewObject(AhObjectToSell obj, string userName);
        Task<AhObjectToSell> GetObject(Guid? id);
        Task DeleteObject(AhObjectToSell ObjectToSell);
        Task<bool> EditObject(AhObjectToSell ObjectToSell);
    }
}
