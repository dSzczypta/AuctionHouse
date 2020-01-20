using AuctionHouseCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseCore.Services
{
    public class PaymentMethodManager : IPaymentMethodManager
    {
        private readonly AuctionHouseContext _context;
        public PaymentMethodManager()
        {
            _context = new AuctionHouseContext();
        }

        public async Task<IList<AhPaymentMethod>> GetPaymentMethod() =>
            await _context.AhPaymentMethod.ToListAsync();

        public async Task<AhPaymentMethod> GetPaymentMethod(Guid? id) =>
            await _context.AhPaymentMethod.FirstOrDefaultAsync(m => m.Id == id);

        public async Task EditPayment(AhPaymentMethod paymentMethod)
        {
            _context.Attach(paymentMethod).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public bool AhPaymentMethodExists(Guid id) =>
            _context.AhPaymentMethod.Any(e => e.Id == id);

        public async Task DeletePaymentMethod(Guid? id)
        {
            if (id == null)
            {
                return;
            }


            var AhPaymentMethod = await _context.AhPaymentMethod.FindAsync(id);

            if (AhPaymentMethod != null)
            {
                _context.AhPaymentMethod.Remove(AhPaymentMethod);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddPaymentMethod(AhPaymentMethod paymentMethod)
        {
            _context.AhPaymentMethod.Add(paymentMethod);
            await _context.SaveChangesAsync();
        }
    }
}
