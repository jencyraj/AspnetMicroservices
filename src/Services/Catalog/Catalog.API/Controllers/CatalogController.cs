using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Repositories;
using Microsoft.Extensions.Logging;
using Catalog.API.Entities;
using System.Net;

namespace Catalog.API.Controllers
{
    // presentation Layer
    // Ist i convert the controller to Api controller
    [ApiController]
    [Route("api/v1/[controller]")]// it is going to get the controller , gemove the controller and get to  name is controler.
    
    //inherit the controller base in order to use these API related object instead.
    public class CatalogController:ControllerBase 
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        // API method
        // Here I used HTTP method first
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>>GetProducts()
        {
            var products =await _repository.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductsById(string id)
        {
            var product = await _repository.GetProduct(id);

            if(product==null)
            {
                _logger.LogError($"Product with id : {id}, not found!.");
                return NotFound();
            }
            return Ok(product);

        }
        [Route("[action]/{category}",Name = "GetProductsByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>>GetProductsByCategory(string category)
        {
            var products = await _repository.GetProductsByCategory(category);
            return Ok(products);
        }
        //[HttpPost]

    }
}
