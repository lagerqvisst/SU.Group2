﻿using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Employees
{
    public class EmployeeRoleAssignment
    {
        public int EmployeeRoleAssignmentId { get; set; }
        public EmployeeType Role { get; set; }
        public double Percentage { get; set; }

        // Navigeringsegenskap till Employee
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}