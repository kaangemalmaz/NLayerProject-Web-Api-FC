using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Entity.Models;
using UdemyNLayerProject.Entity.Repository;
using UdemyNLayerProject.Entity.Services;
using UdemyNLayerProject.Entity.UnitOfWorks;

namespace UdemyNLayerProject.Business.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<IEnumerable<Product>> GetWithCategory()
        {
            return await _unitOfWork.Product.GetWithCategory();
        }

        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            return await _unitOfWork.Product.GetWithCategoryByIdAsync(productId);
        }
    }
}
