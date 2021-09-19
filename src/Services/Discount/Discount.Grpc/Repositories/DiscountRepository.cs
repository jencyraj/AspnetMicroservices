using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discount.Grpc.Entities;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Dapper;


namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            //create a connection Npga
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            // going to create a coupon informtion
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new {ProductName = productName});
            if(coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };


            }
            return coupon;

        }
       public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var dataaffected =
                await connection.ExecuteAsync("INSERT INTO Coupon (ProductName,Description,Amount) VALUES(@ProductName,@Description,@Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if(dataaffected == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var updated =
                await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName ,Description=@Description, Amount=@Amount WHERE Id=@Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount,Id=coupon.Id });
            if (updated == 0)
                return false;
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var deleted =
                await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName=@ProductName",
                new { ProductName = productName });
            if (deleted == 0)
                return false;
            return true;

        }

    }
    }
