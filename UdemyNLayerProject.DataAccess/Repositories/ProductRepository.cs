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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Product>> GetWithCategory()
        {
            return await context.Set<Product>().Include(i => i.Category).ToListAsync();
        }

        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            return await context.Set<Product>().Include(i => i.Category).FirstOrDefaultAsync(i=>i.Id == productId);
        }

        
    }
}
