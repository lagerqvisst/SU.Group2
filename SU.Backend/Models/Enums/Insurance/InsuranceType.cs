using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Enums.Insurance
{
    public enum InsuranceType
    {
        //Private Types
        ChildAccidentAndHealthInsurance, // Sjuk- och olycksfallsförsäkring för barn (t.o.m. 17 års ålder)
        AdultAccidentAndHealthInsurance, // Sjuk- och olycksfallsförsäkring för vuxen
        AdultLifeInsurance, // Livförsäkring för vuxen

        //Company Types
        PropertyAndInventoryInsurance, // Fastighet och inventarieförsäkring
        VehicleInsurance, // Fordonsförsäkring
        LiabilityInsurance // Ansvarsförsäkring
    }

}

