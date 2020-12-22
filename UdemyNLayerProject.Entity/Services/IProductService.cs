using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Entity.Models;

namespace UdemyNLayerProject.Entity.Services
{
    public interface IProductService : IService<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int productId);
        Task<IEnumerable<Product>> GetWithCategory();
    }
}
