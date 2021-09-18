using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository:IBasketRepository // inherits from the IBasketrepositoy 
    {
        // here we are goig to perform interface method inside of this basket reposirory
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }
        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);// we gt the useName information as Json format to basket obj
            if(string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject < ShoppingCart > (basket);


        }
        //Update
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
             await _redisCache.SetStringAsync(basket.UserName,JsonConvert.SerializeObject(basket)); // will get oll the information and username.
            return await GetBasket(basket.UserName);
        }
        //Delete
        public async Task DeleteBasket(String userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
        }
}
