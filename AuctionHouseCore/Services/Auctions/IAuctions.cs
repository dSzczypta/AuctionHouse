using AuctionHouseCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public interface IAuctions
    {
        Task<List<AhPaymentMethod>> GetPaymentMethods();
        Task<List<AhShipmentType>> GetShipmentTypes();
        Task<string> BuyNow(AhAuctions auction);

    }
}
