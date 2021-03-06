﻿using AuctionHouseCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public interface IAuctions
    {
        Task<string> BuyNow(AhAuctions auction);
        Task<AhAuctions> GetAuctions(Guid? id);
        Task DeleteAuction(AhAuctions auction);
        Task<IList<AhAuctions>> GetAuctions();
        Task AcceptAuction(Guid? id);
    }
}
