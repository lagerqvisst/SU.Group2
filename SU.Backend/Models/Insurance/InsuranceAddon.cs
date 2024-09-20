using SU.Backend.Models.Enums.Insurance.Addons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurance
{
    public class InsuranceAddon
    {
        public int InsuranceAddonId { get; set; } // PK
        
        public int InsuranceAddonTypeId { get; set; } // FK
        public InsuranceAddonType InsuranceAddonType { get; set; } // Navigation property

        // Foreign key to PrivateInsurance
        public int PrivateInsuranceId { get; set; } // FK
        public Insurance Insurance { get; set; } // Navigation property

        // Beräkna tilläggspremien baserat på InsuranceAddonType
        //Flytta ut till service layer senare 

        /*
        public decimal CalculateExtraPremium()
        {
            decimal extraPremium = 0;

            switch (InsuranceAddonType)
            {
                case InsuranceAddonType.SicknessAccident:
                    if (SicknessAccidentOption.HasValue)
                    {
                        extraPremium = (int)SicknessAccidentOption * 0.0003m; // Exempel på beräkning
                    }
                    break;

                case InsuranceAddonType.LongTermSickness:
                    if (LongTermSicknessOption.HasValue)
                    {
                        extraPremium = (int)LongTermSicknessOption * 0.005m; // Exempel på beräkning
                    }
                    break;

                default:
                    throw new InvalidOperationException("Unknown insurance addon type.");
            }

            return extraPremium;
        } */
    }


}
