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
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IRepository<Category> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Category> GetWithProductByIdAsync(int categoryId)
        {
            return await _unitOfWork.Category.GetWithProductByIdAsync(categoryId);
        }
    }
}
