using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discount.API.Entities;

namespace Discount.API.Repositories
{

    // will be represent the discount related operations.
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);

        // Crud operations 
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);





    }
}
