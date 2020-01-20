using AuctionHouseCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public class ShipmentTypeManager : IShipmentTypeManager
    {
        AuctionHouseContext _context;
        public ShipmentTypeManager()
        {
            _context = new AuctionHouseContext();
        }

        public async Task<IList<AhShipmentType>> GetShipmentType() =>
            await _context.AhShipmentType.ToListAsync();

        public async Task<AhShipmentType> GetShipmentType(Guid? id) =>
            await _context.AhShipmentType.FirstAsync(x => x.Id == id);

        public async Task EditShipmentType(AhShipmentType shipmentType)
        {
            _context.Attach(shipmentType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch 
            {
                    throw;
            }
        }

        public bool AhShipmentTypeExists(Guid? id) =>
            _context.AhShipmentType.Any(e => e.Id == id);

        public async Task DeleteShipmentType(Guid? id)
        {
            var shipmentType = await _context.AhShipmentType.FindAsync(id);
            if (shipmentType != null)
            {
                _context.AhShipmentType.Remove(shipmentType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateShipmentType(AhShipmentType shipmentType)
        {
            _context.AhShipmentType.Add(shipmentType);
            await _context.SaveChangesAsync();
        }
    }
}
