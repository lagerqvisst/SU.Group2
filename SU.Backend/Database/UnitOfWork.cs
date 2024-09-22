using SU.Backend.Database.Repositories;
using SU.Backend.Models;
using SU.Backend.Models.Employee;
using SU.Backend.Models.Insurance;

namespace SU.Backend.Database
{
    public class UnitOfWork : IDisposable
    {
        private readonly Context _context;
        public EmployeeRepository Employees { get; }
        public PrivateCustomerRepository PrivateCustomers { get; }
        public CompanyCustomerRepository CompanyCustomers { get; }
        public PrivateCoverageOptionRepository PrivateCoverageOptions { get; }
        public InsurancePolicyHolderRepository InsurancePolicyHolders { get; }
        public InsuredPersonRepository InsuredPersons { get; }
        public PrivateCoverageRepository PrivateCoverages { get; }
        public InsuranceCoverageRepository InsuranceCoverages { get; }

        public InsuranceRepository Insurances { get; }



        public UnitOfWork(Context context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            PrivateCustomers = new PrivateCustomerRepository(_context);
            PrivateCoverageOptions = new PrivateCoverageOptionRepository(_context);
            InsurancePolicyHolders = new InsurancePolicyHolderRepository(_context);
            InsuredPersons = new InsuredPersonRepository(_context);
            PrivateCoverages = new PrivateCoverageRepository(_context);
            InsuranceCoverages = new InsuranceCoverageRepository(_context);
            Insurances = new InsuranceRepository(_context);

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
