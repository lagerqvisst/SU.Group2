﻿using SU.Backend.Models.Employee;
using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<(bool Success, string Message, Employee Employee)> GenerateRandomEmployee(EmployeeType Role);
    }
}