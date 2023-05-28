using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public interface IUnitOfWork
    {
        public ICustomerRepository Customer { get; }
        public IProductRepository Product { get; }
        public ICategoryRepository Category { get; }
        public IPostRepository Post { get; }
        public IMarketPriceRepository MarketPrice { get; }

        Task<int> SaveChanges();
        IDbContextTransaction Transaction();
    }
}
