using SU.Backend.Models.Employees;

namespace SU.Frontend.Helper.DI_Objects.User;

public class LoggedInUserService : ILoggedInUserService
{
    public Employee LoggedInEmployee { get; set; }
}