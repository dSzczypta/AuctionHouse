using AuctionHouseCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<string> AddNewObject(AhObjectToSell obj, string userName)
        {
            var id = Guid.NewGuid();
            try
            {
                obj.Id = id;
                obj.ImagePath = id.ToString() + ".jpg";
                obj.AddedBy = userName;
                obj.DateAdded = DateTime.Now;
                _context.AhObjectToSell.Add(obj);
                await _context.SaveChangesAsync();
                return id.ToString();
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

        public async Task<List<AhObjectToSell>> GetObjects() =>
            await _context.AhObjectToSell.Where(x => x.Sold == false).ToListAsync();

        public async Task SaveImage(IFormFile file, string path)
        {
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }


    }
}
