using Data.Entities;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FarmerConnectContext _context;

        

        private ICustomerRepository _customer = null!;

        public UnitOfWork(FarmerConnectContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customer
        {
            get { return _customer ??= new CustomerRepository(_context); }
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
