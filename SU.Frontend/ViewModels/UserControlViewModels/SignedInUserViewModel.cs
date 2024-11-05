using SU.Backend.Helper;
using SU.Backend.Models.Employees;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.User;

public class SignedInUserViewModel : ObservableObject
{
    // Provides information about the currently logged-in user
    private readonly ILoggedInUserService _loggedInUserService;

    // The currently logged-in employee's information
    public Employee _loggedInEmployee;

    // Constructor to initialize the logged-in employee 
    public SignedInUserViewModel(ILoggedInUserService loggedInUserService)
    {
        _loggedInUserService = loggedInUserService;
        _loggedInEmployee = _loggedInUserService.LoggedInEmployee;
    }

    // Returns the logged-in user's name as a formatted string
    public string SignedInUserName => $"User logged in: {_loggedInEmployee.FirstName} {_loggedInEmployee.LastName}";

    // Returns the logged-in user's ID as a formatted string
    public string SignedInUserId => $"User ID: {_loggedInEmployee.EmployeeId}";

    // Returns the logged-in user's highest percentage role via method, or 'None' if no role is assigned
    public string SignedInUserRole
    {
        get
        {
            var role = EmployeeHelper.GetLowestPercentageRole(_loggedInEmployee.RoleAssignments.ToList());
            return role != null ? $"User role: {role}" : "User role: None";
        }
    }
}