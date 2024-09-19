﻿using SU.Backend.Models.Insurrance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Customers
{
    public class PrivateCustomer
    {
        public int PrivateCustomerId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // Navigational property for related PrivateInsurance
        public ICollection<PrivateInsurance> PrivateInsurances { get; set; } = new List<PrivateInsurance>();
    }

}
