using SU.Backend.Database.Repositories;
using SU.Backend.Models;
using SU.Backend.Models.Employee;

namespace SU.Backend.Database
{
    public class UnitOfWork : IDisposable
    {
        private readonly Context _context;
        public EmployeeRepository Employees { get; }
        public PrivateCustomerRepository PrivateCustomers { get; }
        public PrivateCoverageOptionRepository PrivateCoverageOptions { get; }


        public UnitOfWork(Context context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            PrivateCustomers = new PrivateCustomerRepository(_context);
            PrivateCoverageOptions = new PrivateCoverageOptionRepository(_context);

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
