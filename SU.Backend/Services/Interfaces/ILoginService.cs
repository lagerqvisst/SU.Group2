using SU.Backend.Models.Employees;

namespace SU.Backend.Services.Interfaces;

/// <summary>
///     This interface is responsible for defining the methods that the LoginService class must implement.
/// </summary>
public interface ILoginService
{
    Task<(bool success, string message, Employee employee)> Authentication(string userName, string password);
}