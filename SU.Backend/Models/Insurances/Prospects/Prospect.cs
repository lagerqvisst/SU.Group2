using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Models.Enums.Prospects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU.Backend.Models.Insurances.Prospects
{
    public class Prospect
    {


        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string PersonalOrOrgNumber { get; set; }
        public string StreetAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AgentNumber { get; set; } // Agent number of the agent that created the prospect
        public string Seller { get; set; } // Name of the seller




    }


}
