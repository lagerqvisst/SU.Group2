﻿using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace SU.Backend.Database.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the EmployeeRepository class must implement.
    /// </summary>
    public interface IEmployeeRepository
    {
        Task<bool> IsAgentNumberUnique(string agentNumber);

        Task<Employee?> GetEmployeeByRole(EmployeeType role);

        Task<Employee?> GetEmployeeByUserCredentials(string usersUsername, string usersPassword);

        Task<List<Employee>> GetSalesEmployees();

        Task<Employee> GetEmployeeById(int id);

        Task <List<Employee>> GetAllEmployees();

        Task<List<EmployeeRoleAssignment>> GetAllEmployeeRoleAssignments();

        Task<EmployeeType> GetRoleByEmployee(Employee employee);
    }
}
