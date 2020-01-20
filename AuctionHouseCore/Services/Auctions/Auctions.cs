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

        public async Task<string> BuyNow(AhAuctions auction)
        {
            var guid = Guid.NewGuid();
            try
            {
                var shipmentPrice = _context.AhShipmentType.First(x => x.Id == auction.ShipmentType).Price;
                var obj = _context.AhObjectToSell.First(x => x.Id == auction.ObjectId);
                var price = obj.Price + shipmentPrice;
                auction.Price = price;
                auction.Id = guid;
                obj.Sold = true;
                _context.AhAuctions.Add(auction);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return guid.ToString();
        }

        public async Task<AhAuctions> GetAuctions(Guid? id) =>
            await _context.AhAuctions
                .Include(a => a.ObjectNavigation)
                .Include(a => a.PaymentMethodNavigation)
                .Include(a => a.ShipmentTypeNavigation).FirstOrDefaultAsync(m => m.Id == id);

        public async Task DeleteAuction(AhAuctions auction)
        {

            var objToSell = await _context.AhObjectToSell.FirstAsync(x => x.Id == auction.ObjectId);
            objToSell.Sold = false;

            _context.AhAuctions.Remove(auction);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<AhAuctions>> GetAuctions() =>
            await _context.AhAuctions
                .Include(a => a.ObjectNavigation)
                .Include(a => a.PaymentMethodNavigation)
                .Include(a => a.ShipmentTypeNavigation).ToListAsync();

        public async Task AcceptAuction(Guid? id)
        {
            var auction = _context.AhAuctions.First(x => x.Id == id);
            auction.IsConfirmed = true;
            await _context.SaveChangesAsync();
        }
    }
}

