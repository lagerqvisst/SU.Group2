using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances
{
    public class InsuredPerson
    {
        /// <summary>
        /// En kund (person eller företag) kan vara försäkringstagare och/eller försäkrad.

        /// </summary>
        public int InsuredPersonId { get; set; } // PK
        public string Name { get; set; }
        public string PersonalNumber { get; set; } // Personnummer

        // Om en försäkrad person återfinns på flera försäkringar, så kan det vara bra att ha en samling av försäkringar.
        public ICollection<PrivateCoverage> PrivateCoverages { get; set; } // Samling av försäkringar


    }

}
