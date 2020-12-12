using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Entity.Models;

namespace UdemyNLayerProject.Entity.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<Category> GetWithProductByIdAsync(int categoryId);
    }
}
