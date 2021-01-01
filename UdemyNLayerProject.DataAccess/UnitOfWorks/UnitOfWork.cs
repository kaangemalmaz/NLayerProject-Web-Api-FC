using System.Threading.Tasks;
using UdemyNLayerProject.DataAccess.Repositories;
using UdemyNLayerProject.Entity.Repository;
using UdemyNLayerProject.Entity.UnitOfWorks;

namespace UdemyNLayerProject.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IProductRepository Product => _productRepository = _productRepository ?? new ProductRepository(_context);

        public ICategoryRepository Category => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
