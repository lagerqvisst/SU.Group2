﻿using SU.Backend.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper.User
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public Employee LoggedInEmployee { get; set; }
    }
}
