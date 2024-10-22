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

    private string _signedInUserId;

    public string SignedInUserId
    {
        get => $" User ID: {_loggedInEmployee.EmployeeId}";
        set
        {
            _signedInUserId = value;
            OnPropertyChanged();
        }
    }

    private string _signedInUserRole;

    public string SignedInUserRole
    {
        get => $" User role: {_loggedInEmployee.RoleAssignments.FirstOrDefault(x => x.Role != null).ToString()}";
        set
        {
            _signedInUserRole = value;
            OnPropertyChanged();
        }
    }


}
