using AuctionHouseCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public interface IShipmentTypeManager
    {
        Task<IList<AhShipmentType>> GetShipmentType();
        Task<AhShipmentType> GetShipmentType(Guid? id);
        Task EditShipmentType(AhShipmentType shipmentType);
        bool AhShipmentTypeExists(Guid? id);
        Task DeleteShipmentType(Guid? id);
        Task CreateShipmentType(AhShipmentType shipmentType);

    }
}
