using SU.Backend.Models.Enums;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task CreateRandomNewEmployee(EmployeeType Role)
        {
            var result = await _employeeService.GenerateRandomEmployee(Role);

            if (result.Success)
            {
                Console.WriteLine($"Employee created successfully:\n{result.Employee}");
            }
            else
            {
                Console.WriteLine($"Error creating employee: {result.Message}");
            }

        }
    }
}
