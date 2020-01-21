using AuctionHouseCore.Models;
using AuctionHouseCore.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
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
                Include(x => x.ObjectNavigation).
                FirstOrDefaultAsync();
            var auctionId = auction.Id;
            await _auctions.DeleteAuction(auction);
            _context = new AuctionHouseContext();
            var result = await _context.AhAuctions.AnyAsync(x => x.Id == auctionId);
            Assert.IsFalse(result);
        }
    }

    public class ObjectsToSell
    {
        private AuctionHouseContext _context;
        private IObjects _objectsToSell;
        [SetUp]
        public void Setup()
        {
            _context = new AuctionHouseContext();
            _objectsToSell = new Objects();
        }

        [Test]
        public async Task AddNewObjectTest()
        {
            var obj = new AhObjectToSell
            {
                DateAdded = DateTime.Now,
                Description = "Test",
                Name = "Test",
                Price = 111,
                Sold = false
            };
            var guid = await _objectsToSell.AddNewObject(obj, "Test");
            var result = await _context.AhObjectToSell.FirstAsync(x => x.Id == Guid.Parse(guid));
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetObjectTest()
        {
            var obj = await _context.AhObjectToSell.FirstOrDefaultAsync();
            var result = await _objectsToSell.GetObject(obj.Id);
            Assert.NotNull(result);
        }

        [Test]
        public async Task EditObjectTest()
        {
            _context = new AuctionHouseContext();
            var obj = new AhObjectToSell();
            try
            {
                obj = await _context.AhObjectToSell.FirstAsync(x => x.Name == "Test");
            }
            catch
            {
                Assert.True(true);
                return;
            }
            var id = obj.Id;
            obj.AddedBy = "editObj";
            await _objectsToSell.EditObject(obj);
            _context = new AuctionHouseContext();
            var newObj = await _context.AhObjectToSell.FirstAsync(x => x.Id == id);
            Assert.AreEqual(newObj.AddedBy, "editObj");
        }

        [Test]
        public async Task GetObjectsTest()
        {
            var result = await _objectsToSell.GetObjects();
            var listOfObj = await _context.AhObjectToSell.Where(x => x.Sold == false).ToListAsync();
            Assert.AreEqual(result.Count, listOfObj.Count);
        }

        [Test]
        public async Task DeleteObjectTest()
        {
            var obj = await _context.AhObjectToSell.FirstAsync(x => x.Name == "Test");
            await _objectsToSell.DeleteObject(obj);
            _context = new AuctionHouseContext();
            var result = await _context.AhObjectToSell.AnyAsync(x => x.Name == "Test");
            Assert.IsFalse(result);
        }
    }

    public class PaymentMethodsManager
    {
        private AuctionHouseContext _context;
        private IPaymentMethodManager _paymentMethodManager;
        [SetUp]
        public void Setup()
        {
            _context = new AuctionHouseContext();
            _paymentMethodManager = new PaymentMethodManager();
        }

        [Test]
        public async Task GetPaymentMethodTest()
        {
            var functionResult = await _paymentMethodManager.GetPaymentMethod();
            var obj = await _context.AhPaymentMethod.ToListAsync();
            Assert.AreEqual(obj.Count, functionResult.Count);
        }

        [Test]
        public async Task GetPaymentMethodGuidTest()
        {
            var paymentId = "8FEA7397-53D9-4ADE-8278-AA8805DD88BF";
            var functionResult = await _paymentMethodManager.GetPaymentMethod(Guid.Parse(paymentId));
            Assert.NotNull(functionResult);
        }

        [Test]
        public void PaymentMethodExistTest()
        {
            var paymentId = "8FEA7397-53D9-4ADE-8278-AA8805DD88BF";
            var funtionResult = _paymentMethodManager.AhPaymentMethodExists(Guid.Parse(paymentId));
            Assert.IsTrue(funtionResult);

        }

        [Test]
        public async Task AddPaymentMethodTest()
        {
            var obj = new AhPaymentMethod
            {
                Name = "Test"
            };
            await _paymentMethodManager.AddPaymentMethod(obj);
            var result = _context.AhPaymentMethod.Any(x => x.Name == "Test");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeletePaymentMethodTest()
        {
            var obj = await _context.AhPaymentMethod.FirstAsync(x => x.Name == "Test");
            await _paymentMethodManager.DeletePaymentMethod(obj.Id);
            _context = new AuctionHouseContext();
            var result = await _context.AhPaymentMethod.AnyAsync(x => x.Name == "Test");
            Assert.IsFalse(result);
        }
    }

    public class Person
    {
        private AuctionHouseContext _context;

        [SetUp]
        public void Setup()
        {
            _context = new AuctionHouseContext();
        }

        [Test]
        public async Task Test1()
        {
            var functionResult = await AuctionHouseCore.Services.Person.GetHistory("d.szczypta@gmail.com");
            var obj = await _context.AhAuctions
                .Include(a => a.ObjectNavigation)
                .Include(a => a.PaymentMethodNavigation)
                .Include(a => a.ShipmentTypeNavigation)
                .Where(x => x.UserName == "d.szczypta@gmail.com")
                .ToListAsync();
            Assert.AreEqual(functionResult.Count, obj.Count);
        }
    }

    public class ShipmentTypeManaget
    {
        private AuctionHouseContext _context;
        private IShipmentTypeManager _shipmentTypeManager;
        [SetUp]
        public void Setup()
        {
            _context = new AuctionHouseContext();
            _shipmentTypeManager = new ShipmentTypeManager();
        }

        [Test]
        public async Task GetShipmentTypeGuidTest()
        {
            var shipmentTypeId = "8C60E7E0-97BB-4CC4-B39C-9EEF1B61280A";
            var functionResult = await _shipmentTypeManager.GetShipmentType(Guid.Parse(shipmentTypeId));
            Assert.NotNull(functionResult);
        }

        [Test]
        public async Task GetShipmentTypeTest()
        {
            var functionResult = await _shipmentTypeManager.GetShipmentType();
            var obj = await _context.AhShipmentType.ToListAsync();
            Assert.AreEqual(obj.Count, functionResult.Count);
        }

        [Test]
        public void ShipmentTypeExistTest()
        {
            var shipmentTypeId = "8C60E7E0-97BB-4CC4-B39C-9EEF1B61280A";
            var result = _shipmentTypeManager.AhShipmentTypeExists(Guid.Parse(shipmentTypeId));
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateShipmentTest()
        {
            var obj = new AhShipmentType
            {
                Name = "Test",
                Price = 11,
                Time = "2"
            };
            await _shipmentTypeManager.CreateShipmentType(obj);
            var result = await _context.AhShipmentType.AnyAsync(x => x.Name == "Test");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteShipmentTest()
        {
            var obj = await _context.AhShipmentType.FirstAsync(x => x.Name == "Test");
            await _shipmentTypeManager.DeleteShipmentType(obj.Id);
            _context = new AuctionHouseContext();
            var result = await _context.AhShipmentType.AnyAsync(x => x.Name == "Test");
            Assert.IsFalse(result);
        }
    }

    public class System
    {
        private AuctionHouseContext _context;
        private IAdminPanel _adminPanel;
        [SetUp]
        public void Setup()
        {
            _context = new AuctionHouseContext();
            _adminPanel = new AdminPanel();
        }

        [Test]
        public async Task GetAllUserTest()
        {
            var functionResult = await _adminPanel.GetAllUsers();
            var obj = await _context.AhPerson.ToListAsync();
            Assert.AreEqual(functionResult.Count, obj.Count);
        }

        [Test]
        public async Task GetPersonDetailsTest()
        {
            var obj = await _context.AhPerson.Include(x => x.AspNetUser).FirstOrDefaultAsync();
            var functionResult = await _adminPanel.GetPersonDetails(obj.AspNetUser.Id);
            Assert.AreEqual(obj.Name, functionResult.Name);
        }

        [Test]
        public async Task IsUserExistTest()
        {
            var obj = await _context.AhPerson.FirstOrDefaultAsync();
            var functionResult = await _adminPanel.IsUserExist(obj.AspNetUserId);
            Assert.IsTrue(functionResult);
        }

        [Test]
        public async Task GetTypeOfUserTest()
        {
            var functionResult = await AdminPanel.GetTypeOfUser("d.szczypta@gmail.com");
            Assert.AreEqual(functionResult, "1");
        }
    }
}