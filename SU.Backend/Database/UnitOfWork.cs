using SU.Backend.Database.Repositories;
using SU.Backend.Models;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Insurances;

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
        public InsuranceAddonTypeRepository InsuranceAddonTypes { get; }
        public ProspectRepository Prospects { get; }

        public VehicleInsuranceOptionRepository VehicleInsuranceOptions { get; }

        public RiskzoneRepository Riskzones { get; }

        public LiabilityCoverageOptionRepository LiabilityCoverageOptions { get; }



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
            InsuranceAddonTypes = new InsuranceAddonTypeRepository(_context);
            Prospects = new ProspectRepository(_context);
            CompanyCustomers = new CompanyCustomerRepository(_context);
            VehicleInsuranceOptions = new VehicleInsuranceOptionRepository(_context);
            Riskzones = new RiskzoneRepository(_context);
            LiabilityCoverageOptions = new LiabilityCoverageOptionRepository(_context);

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
