using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Entity.Repository;

namespace UdemyNLayerProject.Entity.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }

        Task CommitAsync();
        void Commit();
    }
}
