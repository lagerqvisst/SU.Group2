using SU.Backend.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    public interface ILoginService
    {
        Task<(bool Success, string Message, Employee Employee)> Authentication(string Username, string Password);
    }
}
