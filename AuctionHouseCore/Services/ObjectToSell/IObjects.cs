using AuctionHouseCore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public interface IObjects
    {
        Task<string> AddNewObject(AhObjectToSell obj, string userName);
        Task<AhObjectToSell> GetObject(Guid? id);
        Task DeleteObject(AhObjectToSell ObjectToSell);
        Task<bool> EditObject(AhObjectToSell ObjectToSell);
        Task<List<AhObjectToSell>> GetObjects();
        Task SaveImage(IFormFile file, string path);
    }
}
