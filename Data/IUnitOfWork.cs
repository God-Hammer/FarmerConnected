using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public interface IUnitOfWork
    {
        public ICustomerRepository Customer { get; }

        Task<int> SaveChanges();
        IDbContextTransaction Transaction();
    }
}
