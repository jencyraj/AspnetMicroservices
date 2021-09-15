using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository // i am going to use some API here-
    {
        Task<IEnumerable<Product>> GetProducts(); // here we are going to expecting listing of a product in a segment
                                                  // thats why I call Task

        Task<Product> GetProduct(string id);// here Id is string because I used Mongo Db
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
    }
}
