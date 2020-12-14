using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Entity.Models;
using UdemyNLayerProject.Entity.Repository;

namespace UdemyNLayerProject.DataAccess.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Category> GetWithProductByIdAsync(int categoryId)
        {
            return await context.Set<Category>().Include(i => i.Products).FirstOrDefaultAsync(i => i.Id == categoryId);
        }
    }
}
