using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Enums.Prospects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Helper
{
    public static class EnumService
    {
        /// <summary>
        /// This class is used to get the different enum values for the different enums in the project.
        /// It was created to easily fetch information in the same way we do in our service classes but for enums which are are not stored in the database.
        /// You can see references in Viewmodels in the frontend project. Many of these enums are used in dropdowns / comboboxes.
        /// </summary>
        /// <returns></returns>
        public static List<RiskzoneLevel> RiskzoneLevels()
        {
            return Enum.GetValues(typeof(RiskzoneLevel)).Cast<RiskzoneLevel>().ToList();
        }

        public static List<VehicleCoverageOptions> VehicleCoverageOptions()
        {
            return Enum.GetValues(typeof(VehicleCoverageOptions)).Cast<VehicleCoverageOptions>().ToList();
        }

        public static List<Salary> Salaries()
        {
            return Enum.GetValues(typeof(Salary)).Cast<Salary>().ToList();
        }

        public static List<EmployeeType> EmployeeType() {
            return Enum.GetValues(typeof(EmployeeType)).Cast<EmployeeType>().ToList();
        }

        public static List<ProspectType> ProspectType() {
            return Enum.GetValues(typeof(ProspectType)).Cast<ProspectType>().ToList();
        }

        public static List<ProspectStatus> ProspectStatus() {
            return Enum.GetValues(typeof(ProspectStatus)).Cast<ProspectStatus>().ToList();
        }

        public static List<PaymentPlan> PaymentPlans()
        {
            return Enum.GetValues(typeof(PaymentPlan)).Cast<PaymentPlan>().ToList();
        }

        public static List<LiabilityCoverageOptionAmounts> LiabilityCoverageOptionAmounts()
        {
            return Enum.GetValues(typeof(LiabilityCoverageOptionAmounts)).Cast<LiabilityCoverageOptionAmounts>().ToList();
        }

        public static List<InsuranceType> InsuranceType() {
            return Enum.GetValues(typeof(InsuranceType)).Cast<InsuranceType>().ToList();
        }

        public static List<InsuranceStatus> InsuranceStatus() {
            return Enum.GetValues(typeof(InsuranceStatus)).Cast<InsuranceStatus>().ToList();
        }

        public static List<Deductible> Deductibles()
        {
            return Enum.GetValues(typeof(Deductible)).Cast<Deductible>().ToList();
        }

        public static List<AddonType> AddonTypes()
        {
            return Enum.GetValues(typeof(AddonType)).Cast<AddonType>().ToList();
        }

        public static List<LongTermSicknessOption> LongTermSicknessOptions()
        {
            return Enum.GetValues(typeof(LongTermSicknessOption)).Cast<LongTermSicknessOption>().ToList();
        }

        public static List<SicknessAccidentOption> SicknessAccidentOptions() {
            return Enum.GetValues(typeof(SicknessAccidentOption)).Cast<SicknessAccidentOption>().ToList();
        }
    }
}
