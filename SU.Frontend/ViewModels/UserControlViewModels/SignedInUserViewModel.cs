﻿using SU.Backend.Models.Employees;
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
            var roleAssignment = _loggedInEmployee?.RoleAssignments?.FirstOrDefault(x => x.Role != null);
            return roleAssignment != null ? $"User role: {roleAssignment.Role.ToString()}" : "User role: None";
        }
    }

}
