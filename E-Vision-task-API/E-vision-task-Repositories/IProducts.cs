using E_Vision_task.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Vision_task.Data
{
    public interface IProducts
    {
        Product Add(Product product);
        Task<Product> UpdateAsync(Product entity);
        Product Delete(int id);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);


    }
}
