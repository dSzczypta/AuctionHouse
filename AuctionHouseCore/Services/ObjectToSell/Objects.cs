using AuctionHouseCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public class Objects : IObjects
    {
        private readonly AuctionHouseContext _context;

        public Objects()
        {
            _context = new AuctionHouseContext();
        }

        public async Task AddNewObject(AhObjectToSell obj, string userName)
        {
            try
            {
                obj.AddedBy = userName;
                obj.DateAdded = DateTime.Now;
                obj.AddedBy = WindowsIdentity.GetCurrent().Name;
                _context.AhObjectToSell.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AhObjectToSell> GetObject(Guid? id) =>
           await _context.AhObjectToSell.FirstOrDefaultAsync(m => m.Id == id);

        public async Task DeleteObject(AhObjectToSell ObjectToSell)
        {
            _context.AhObjectToSell.Remove(ObjectToSell);
            await _context.SaveChangesAsync();
        }

        private bool IsObjExist(Guid? id) =>
           _context.AhObjectToSell.Any(e => e.Id == id);

        public async Task<bool> EditObject(AhObjectToSell ObjectToSell)
        {
            _context.Attach(ObjectToSell).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsObjExist(ObjectToSell.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

        }



    }
}
