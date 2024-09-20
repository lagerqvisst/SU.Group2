using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurance
{
    public class InsuredPerson
    {
        /// <summary>
        /// En kund (person eller företag) kan vara försäkringstagare och/eller försäkrad.

        /// </summary>
        public int InsuredPersonId { get; set; } // PK
        public string Name { get; set; }
        public string PersonalNumber { get; set; } // Personnummer

        // Foreign key for the related policyholder
        public int? InsurancePolicyHolderId { get; set; } // FK

        // Navigation property to the policyholder
        public InsurancePolicyHolder? InsurancePolicyHolder { get; set; }
    }

}
