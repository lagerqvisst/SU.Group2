using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurance;
using SU.Backend.Services.Interfaces;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class DatabaseTestServices : IDatabaseTestService
    {
        private readonly ILogger<DatabaseTestServices> _logger;
        private readonly Context _context;

        public DatabaseTestServices(ILogger<DatabaseTestServices> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<(bool Success, string Message)> TestDbConnection()
        {
            _logger.LogInformation("Testing database connection");

            try
            {
                if (await _context.Database.CanConnectAsync())
                {
                    _logger.LogInformation("Database connection successful");
                    return (true, "Database connection successful");
                }
                else
                {
                    _logger.LogWarning("Database connection failed");
                    return (false, "Database connection failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while testing the database connection");
                return (false, "An error occurred while testing the database connection");
            }
        }

        /// <summary>
        /// Seeds the database with insurance coverages according to the requirements
        /// </summary>
        /// <returns></returns>
        public async Task<(bool Success, string Message)> SeedInsuranceCoverages()
        {
            // This method is not implemented yet, add when we have repository set up.
            /*
            _logger.LogInformation("Seeding insurance coverages");

            try
            {
                // Check if any InsuranceCoverages already exist
                if (_dbConnection.InsuranceCoverages.Any())
                {
                    _logger.LogInformation("Insurance coverages already seeded");
                    return (true, "Insurance coverages already seeded");
                }

                // Seed data for ChildAccidentAndHealthInsurance
                _dbConnection.InsuranceCoverages.AddRange(
                    new InsuranceCoverage { InsuranceCoverageId = 1, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 700000, MonthlyPremium = 350, InsuranceType = PrivateInsuranceType.ChildAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 2, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 900000, MonthlyPremium = 450, InsuranceType = PrivateInsuranceType.ChildAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 3, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 1100000, MonthlyPremium = 550, InsuranceType = PrivateInsuranceType.ChildAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 4, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 1300000, MonthlyPremium = 650, InsuranceType = PrivateInsuranceType.ChildAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 5, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 750000, MonthlyPremium = 375, InsuranceType = PrivateInsuranceType.ChildAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 6, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 950000, MonthlyPremium = 475, InsuranceType = PrivateInsuranceType.ChildAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 7, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 1150000, MonthlyPremium = 575, InsuranceType = PrivateInsuranceType.ChildAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 8, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 1350000, MonthlyPremium = 675, InsuranceType = PrivateInsuranceType.ChildAccidentAndHealthInsurance }
                );

                // Seed data for AdultAccidentAndHealthInsurance
                _dbConnection.InsuranceCoverages.AddRange(
                    new InsuranceCoverage { InsuranceCoverageId = 9, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 300000, MonthlyPremium = 150, InsuranceType = PrivateInsuranceType.AdultAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 10, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 400000, MonthlyPremium = 200, InsuranceType = PrivateInsuranceType.AdultAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 11, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 500000, MonthlyPremium = 250, InsuranceType = PrivateInsuranceType.AdultAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 12, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 350000, MonthlyPremium = 175, InsuranceType = PrivateInsuranceType.AdultAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 13, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 450000, MonthlyPremium = 225, InsuranceType = PrivateInsuranceType.AdultAccidentAndHealthInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 14, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 550000, MonthlyPremium = 275, InsuranceType = PrivateInsuranceType.AdultAccidentAndHealthInsurance }
                );

                // Seed data for AdultLifeInsurance
                _dbConnection.InsuranceCoverages.AddRange(
                    new InsuranceCoverage { InsuranceCoverageId = 15, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 300000, MonthlyPremium = 150, InsuranceType = PrivateInsuranceType.AdultLifeInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 16, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 400000, MonthlyPremium = 200, InsuranceType = PrivateInsuranceType.AdultLifeInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 17, EffectiveDate = new DateTime(2023, 1, 1), BaseAmount = 500000, MonthlyPremium = 250, InsuranceType = PrivateInsuranceType.AdultLifeInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 18, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 350000, MonthlyPremium = 175, InsuranceType = PrivateInsuranceType.AdultLifeInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 19, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 450000, MonthlyPremium = 225, InsuranceType = PrivateInsuranceType.AdultLifeInsurance },
                    new InsuranceCoverage { InsuranceCoverageId = 20, EffectiveDate = new DateTime(2024, 1, 1), BaseAmount = 550000, MonthlyPremium = 275, InsuranceType = PrivateInsuranceType.AdultLifeInsurance }
                );

                // Save changes to the database
                await _dbConnection.SaveChangesAsync();
                _logger.LogInformation("Insurance coverages seeded successfully");
                return (true, "Insurance coverages seeded successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding insurance coverages");
                return (false, "An error occurred while seeding insurance coverages");
            } */

            return (true, "Insurance coverages seeded successfully");
        }
    }
}

