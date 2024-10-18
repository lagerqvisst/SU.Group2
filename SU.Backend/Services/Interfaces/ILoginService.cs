using SU.Backend.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services.Interfaces
{
    /// <summary>
    /// This interface is responsible for defining the methods that the LoginService class must implement.
    /// </summary>
    public interface ILoginService
    {
        Task<(bool Success, string Message, Employee Employee)> Authentication(string Username, string Password);
    }
}
