using E_Vision_task.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Vision_task.Data
{
    public class ProductRepository : IProducts
    {
        private readonly AppDbContext context;
        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Product Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product Delete(int id)
        {
            Product product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(u => u.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> UpdateAsync(Product productchenge)
        {
            if (productchenge == null)
                return null;
            var exists = await context.Products.FirstOrDefaultAsync(u => u.Id == productchenge.Id);
            context.Entry(exists).CurrentValues.SetValues(productchenge);
            context.SaveChanges();
            
            return exists;
        }
    }

}