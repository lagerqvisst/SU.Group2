﻿using Microsoft.EntityFrameworkCore;
using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;

namespace SU.Backend.Database.Repositories;

/// <summary>
///     This class is responsible for implementing the methods defined in the IInsurancePolicyHolderRepository interface.
/// </summary>
public class InsurancePolicyHolderRepository : Repository<InsurancePolicyHolder>, IInsurancePolicyHolderRepository
{
    public InsurancePolicyHolderRepository(Context context) : base(context)
    {
    }
    //This method is used to get a specific insurance policy holder based on the insurance policy holder id
    public async Task<InsurancePolicyHolder> GetById(InsurancePolicyHolder insurancePolicyHolder)
    {
        return await _context.InsurancePolicyHolders.FindAsync(insurancePolicyHolder);
    }

    //This method is used to get all insurance policy holders
    public async Task<List<InsurancePolicyHolder>> GetAllInsurancePolicyHolders()
    {
        return await _context.InsurancePolicyHolders.ToListAsync();
    }
    //This method is used to get all insurance policy holders with insurances
    public async Task<List<InsurancePolicyHolder>> GetAllPolicyHoldersWithInsurances()
    {
        return await _context.InsurancePolicyHolders
            .Include(p => p.Insurance)
            .Include(p => p.CompanyCustomer)
            .ThenInclude(c => c.InsurancePolicyHolders)
            .Include(p => p.PrivateCustomer)
            .ThenInclude(c => c.InsurancePolicyHolders)
            .Where(p => p.Insurance.InsuranceStatus == InsuranceStatus.Active)
            .ToListAsync();
    }
}