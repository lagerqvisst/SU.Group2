using SU.Backend.Models.Employees;

namespace SU.Frontend.Helper.DI_Objects.User;

public interface ILoggedInUserService
{
    Employee LoggedInEmployee { get; set; }
}