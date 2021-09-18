using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.API.Repositories;
using Basket.API.Entities;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }
        [HttpGet("{userName}",Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            // implimentation of API methods
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName)); // if basket is not appear so we create a new shopping cart
                                                    /* purpose of this above code: If the first time client would like to add item
                                                    into basket , we should create a new empty basket. 
                                                    So thats why we are creating shoppingcart object structure in here in the 
                                                    first place. */
            

        }
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody]ShoppingCart basket)
        {
            return Ok(await _repository.UpdateBasket(basket));

        }

        }
}
