using SU.Backend.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper.User
{
    public interface ILoggedInUserService
    {
        Employee LoggedInEmployee { get; set; }
    }
}
