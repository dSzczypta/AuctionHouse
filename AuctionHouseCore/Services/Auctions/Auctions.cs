using AuctionHouseCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public class Auctions : IAuctions
    {
        private readonly AuctionHouseContext _context;
        public Auctions()
        {
            _context = new AuctionHouseContext();
        }

        public Task<List<AhPaymentMethod>> GetPaymentMethods() =>
            _context.AhPaymentMethod.ToListAsync();

        public Task<List<AhShipmentType>> GetShipmentTypes() =>
            _context.AhShipmentType.ToListAsync();

        public async Task<string> BuyNow(AhAuctions auction)
        {
            var guid = Guid.NewGuid();
            try
            {
                var shipmentPrice = _context.AhShipmentType.First(x => x.Id == auction.ShipmentType).Price;
                var objPrice = _context.AhObjectToSell.First(x => x.Id == auction.ObjectId).Price;
                var price = objPrice + shipmentPrice;
                auction.Price = price;
                auction.Id = guid;
                _context.AhAuctions.Add(auction);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return guid.ToString();
        }
    }
}

