using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AuctionHouseCore.Models;

namespace AuctionHouseCore.Services
{
    public interface IPaymentMethodManager
    {
        Task<IList<AhPaymentMethod>> GetPaymentMethod();
        Task<AhPaymentMethod> GetPaymentMethod(Guid? id);
        Task EditPayment(AhPaymentMethod paymentMethod);
        bool AhPaymentMethodExists(Guid id);
        Task DeletePaymentMethod(Guid? id);
        Task AddPaymentMethod(AhPaymentMethod paymentMethod);
    }
}
