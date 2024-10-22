using SU.Backend.Helper;
using SU.Backend.Models.Employees;
using SU.Frontend.Helper;
using SU.Frontend.Helper.User;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

public class SignedInUserViewModel : ObservableObject
{
    private readonly ILoggedInUserService _loggedInUserService;
    public Employee _loggedInEmployee;

    public SignedInUserViewModel(ILoggedInUserService loggedInUserService)
    {
        _loggedInUserService = loggedInUserService;
        _loggedInEmployee = _loggedInUserService.LoggedInEmployee;
    }


    public string SignedInUserName
    {
        get => $"User logged in: {_loggedInEmployee.FirstName} {_loggedInEmployee.LastName}";
    }

    public string SignedInUserId
    {
        get => $"User ID: {_loggedInEmployee.EmployeeId}";
  
    }

    public string SignedInUserRole
    {
        get
        {
            var role = EmployeeHelper.GetHighestPercentageRole(_loggedInEmployee.RoleAssignments.ToList());
            return role != null ? $"User role: {role}" : "User role: None";
        }
    }


}
