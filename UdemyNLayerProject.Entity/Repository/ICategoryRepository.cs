using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Entity.Models;

namespace UdemyNLayerProject.Entity.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    { 
        Task<Category> GetWithProductByIdAsync(int categoryId);
    }
}
