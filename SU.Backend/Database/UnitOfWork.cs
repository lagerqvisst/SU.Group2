using SU.Backend.Database.Repositories;

namespace SU.Backend.Database;

/// <summary>
///     This class is responsible for handling the database context and the repositories.
///     We are using unit of work pattern to handle the database context and repositories.
///     All service classes uses instances of this class to make CRUD against the DB.
/// </summary>
public class UnitOfWork : IDisposable
{
    private readonly Context _context;

    public UnitOfWork(Context context)
    {
        _context = context;
        Employees = new EmployeeRepository(_context);
        PrivateCustomers = new PrivateCustomerRepository(_context);
        PrivateCoverageOptions = new PrivateCoverageOptionRepository(_context);
        InsurancePolicyHolders = new InsurancePolicyHolderRepository(_context);
        PrivateCoverages = new PrivateCoverageRepository(_context);
        InsuranceCoverages = new InsuranceCoverageRepository(_context);
        Insurances = new InsuranceRepository(_context);
        InsuranceAddons = new InsuranceAddonRepository(_context);
        InsuranceAddonTypes = new InsuranceAddonTypeRepository(_context);
        CompanyCustomers = new CompanyCustomerRepository(_context);
        VehicleInsuranceOptions = new VehicleInsuranceOptionRepository(_context);
        VehicleInsuranceCoverages = new VehicleInsuranceCoverageRepository(_context);
        Riskzones = new RiskzoneRepository(_context);
        LiabilityCoverageOptions = new LiabilityCoverageOptionRepository(_context);
        LiabilityCoverages = new LiabilityCoverageRepository(_context);
        PropertyAndInventoryCoverages = new PropertyAndInventoryCoverageRepository(_context);
    }

    public EmployeeRepository Employees { get; }
    public PrivateCustomerRepository PrivateCustomers { get; }
    public CompanyCustomerRepository CompanyCustomers { get; }
    public PrivateCoverageOptionRepository PrivateCoverageOptions { get; }
    public InsurancePolicyHolderRepository InsurancePolicyHolders { get; }
    public PrivateCoverageRepository PrivateCoverages { get; }
    public InsuranceCoverageRepository InsuranceCoverages { get; }
    public InsuranceRepository Insurances { get; }
    public InsuranceAddonTypeRepository InsuranceAddonTypes { get; }
    public InsuranceAddonRepository InsuranceAddons { get; }
    public VehicleInsuranceOptionRepository VehicleInsuranceOptions { get; }
    public RiskzoneRepository Riskzones { get; }
    public LiabilityCoverageOptionRepository LiabilityCoverageOptions { get; }
    public VehicleInsuranceCoverageRepository VehicleInsuranceCoverages { get; }
    public LiabilityCoverageRepository LiabilityCoverages { get; }
    public PropertyAndInventoryCoverageRepository PropertyAndInventoryCoverages { get; }

    public void Dispose()
    {
        _context.Dispose();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}