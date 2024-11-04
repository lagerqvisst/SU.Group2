using SU.Backend.Helper;
using SU.Backend.Models.Employees;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.User;
using System.Linq;

namespace SU.Frontend.ViewModels.UserControlViewModels
{
    public class UserInfoDockViewModel : ObservableObject
    {
        private readonly ILoggedInUserService _loggedInUserService;
        private Employee _loggedInEmployee;

        public UserInfoDockViewModel(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
            LoggedInEmployee = _loggedInUserService.LoggedInEmployee ?? new Employee();
        }

        public Employee LoggedInEmployee
        {
            get => _loggedInEmployee;
            set
            {
                _loggedInEmployee = value;
                OnPropertyChanged(nameof(LoggedInEmployee));
                OnPropertyChanged(nameof(SignedInUserName));
                OnPropertyChanged(nameof(SignedInUserId));
                OnPropertyChanged(nameof(SignedInUserRole));
            }
        }

        public string SignedInUserName
        {
            get => _loggedInEmployee != null
                ? $"User: {_loggedInEmployee.FirstName} {_loggedInEmployee.LastName}"
                : "User logged in: Unknown";
        }

        public string SignedInUserId
        {
            get => _loggedInEmployee != null
                ? $"ID: {_loggedInEmployee.EmployeeId}"
                : "User ID: Unknown";
        }

        public string SignedInUserRole
        {
            get
            {
                if (_loggedInEmployee?.RoleAssignments != null && _loggedInEmployee.RoleAssignments.Any())
                {
                    var role = EmployeeHelper.GetLowestPercentageRole(_loggedInEmployee.RoleAssignments.ToList());
                    return role != null ? $"Role: {role}" : "User role: None";
                }
                return "User role: None";
            }
        }
    }
}
