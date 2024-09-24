using SU.Backend.Database.Interfaces;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class InsuredPersonRepository : Repository<InsuredPerson>, IInsuredPersonRepository
    {
        public InsuredPersonRepository(Context context) : base(context)
        {
        }

        public async Task<InsuredPerson> GetById(InsuredPerson insuredPerson)
        {
            return _context.InsuredPersons.FirstOrDefault(x => x.InsuredPersonId == insuredPerson.InsuredPersonId);
        }
    }

}
