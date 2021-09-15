using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Catalog.API.Entities;


namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any(); //Any part of the collection has existing in this collection
                                                                         // we can continue the operation
                                                                         //If we  cannot find any documentaion from the collection,
                                                                         // we are going to see a database for that
            if(!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }

                                                                         
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                  Id = "614251e44194805f57e84913",
                  Name = "Hp Laptop",
                  Category= "Computer",
                  Summary ="testing summary",
                  Discription="testing des",
                  ImageFile ="imagefile1",
                  Price = 2000.00M,
                 
                },
                new Product()
                {
                  Id = "614251e44194805f57e84914",
                  Name = "Lenovo Laptop",
                  Category= "Computer",
                  Summary ="testing summary",
                  Discription="testing des",
                  ImageFile ="imagefile2",
                  Price = 25000.00M,

                },
                new Product()
                {
                  Id = "614251e44194805f57e84915",
                  Name = "Mi note",
                  Category= "Mobile",
                  Summary ="testing summary",
                  Discription="testing des",
                  ImageFile ="imagefile1",
                  Price = 1500.00M,

                },
                new Product()
                {
                  Id = "614251e44194805f57e84916",
                  Name = "Avast",
                  Category= "Antivirus",
                  Summary ="testng summary",
                  Discription="testing des",
                  ImageFile ="imagefile4",
                  Price = 600.00M,

                }
            };
        }
    }
}
