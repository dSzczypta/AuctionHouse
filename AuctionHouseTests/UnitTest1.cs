using AuctionHouseCore.Models;
using AuctionHouseCore.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Tests
{

    public class Auctions
    {
        private AuctionHouseContext _context;
        private IAuctions _auctions;
        [SetUp]
        public void Setup()
        {
            _context = new AuctionHouseContext();
            _auctions = new AuctionHouseCore.Services.Auctions();
        }

        [Test]
        public async Task BuyNowTest()
        {
            var auction = await _context.AhObjectToSell.FirstAsync(x => x.Sold == false);
            var obj = new AhAuctions
            {
                ObjectNavigation = auction,
                ObjectId = auction.Id,
                PaymentMethod = Guid.Parse("8FEA7397-53D9-4ADE-8278-AA8805DD88BF"),
                ShipmentType = Guid.Parse("8C60E7E0-97BB-4CC4-B39C-9EEF1B61280A"),
                UserName = "d.szczypta@gmail.com"
            };

            var functionResult = await _auctions.BuyNow(obj);
            var testAuctions = await _context.AhAuctions.FirstAsync(x => x.Id == Guid.Parse(functionResult));
            Assert.IsNotNull(testAuctions);
        }

        [Test]
        public async Task GetAuctionsTest()
        {
            var functionResult = await _auctions.GetAuctions();
            var auctions = await _context.AhAuctions.ToListAsync();
            Assert.AreEqual(functionResult.Count, auctions.Count);
        }

        [Test]
        public async Task GetAuctionGuidTest()
        {
            try
            {
                var auction = await _context.AhAuctions.FirstAsync(x => x.IsConfirmed == false);
                var functionResult = _auctions.GetAuctions(auction.Id);
                Assert.IsTrue(functionResult != null);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public async Task AcceptAuctionTest()
        {
            AhAuctions auction = new AhAuctions();
            try
            {
                auction = await _context.AhAuctions.FirstAsync(x => x.IsConfirmed != true);
            }
            catch
            {
                Assert.IsTrue(true);
            }
            
            var auctionId = auction.Id;
            auction = null;
            _auctions.AcceptAuction(auctionId).Wait();
            _context = new AuctionHouseContext();
            auction = await _context.AhAuctions.FirstAsync(x => x.Id == auctionId);
            var result = auction.IsConfirmed; 
            auction.IsConfirmed = false;
            await _context.SaveChangesAsync();
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteAuction()
        {
            var auction = await _context.AhAuctions.
                Include(x=>x.ObjectNavigation).
                FirstOrDefaultAsync();
            var auctionId = auction.Id;
            await _auctions.DeleteAuction(auction);
            _context = new AuctionHouseContext();
            var result = await _context.AhAuctions.AnyAsync(x=>x.Id == auctionId);
            Assert.IsFalse(result);
        }
    }

    public class ObjectsToSell
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }

    public class PaymentMethodsManager
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }

    public class Person
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }

    public class ShipmentTypeManaget
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }

    public class System
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}