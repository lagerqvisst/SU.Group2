using SU.Backend.Database;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurance.Coverage;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Context _context;

        public InsuranceService(UnitOfWork unitOfWork, Context context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<(bool Success, string Message)> SeedPrivateInsuranceOptions()
        {
            try
            {
                PrivateCoverageOption Option =
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 1,
                    CoverageAmount = 700_000m,
                    MonthlyPremium = 350m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance
                };

                _unitOfWork.PrivateCoverageOptions.Add(Option);
                _unitOfWork.SaveChanges();

                return (true, "Private insurance options seeded successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
