using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context; // this is an data abstraction,  I need to use data operations

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // Generating the IProductRepository
      
        public async Task<IEnumerable<Product>> GetProducts()
        {
            // p=>0 its a condition, we can filter the conditions in these different object
            return await _context
                    .Products
                    .Find(p => true)
                    .ToListAsync();
                 
        }
        public async Task<Product> GetProduct( String id)
        {
            return await _context
                    .Products
                    .Find(p =>p.Id == id)
                    .FirstOrDefaultAsync();

        }
        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context
                    .Products
                    .Find(filter)
                    .ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _context
                    .Products
                    .Find(filter)
                    .ToListAsync();

        }
        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
           var updateResult = await _context
                    .Products
                    .ReplaceOneAsync(filter: f => f.Id == product.Id, replacement: product);
           return  updateResult.IsAcknowledged && updateResult.ModifiedCount < 0;

                    

        }
    }
}
