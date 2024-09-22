using SU.Backend.Models.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Database.Repositories
{
    public class InsuranceRepository: Repository<Insurance>
    {
        public InsuranceRepository(Context context) : base(context)
        {
        }

    }

}
