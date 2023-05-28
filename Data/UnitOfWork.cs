using Data.Entities;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FarmerConnectContext _context;

        

        private ICustomerRepository _customer = null!;
        private IProductRepository _product = null!;
        private ICategoryRepository _category = null!;

        public UnitOfWork(FarmerConnectContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customer
        {
            get { return _customer ??= new CustomerRepository(_context); }
        }

        public IProductRepository Product
        {
            get { return _product ??= new ProductRepository(_context); }
        }

        public ICategoryRepository Category
        {
            get { return _category ??= new CategoryRepository(_context); }
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public IDbContextTransaction Transaction()
        {
            return _context.Database.BeginTransaction();
        }

    }
}
