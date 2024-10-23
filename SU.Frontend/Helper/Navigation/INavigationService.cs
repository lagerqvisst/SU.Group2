﻿using SU.Backend.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper.Navigation
{
    public interface INavigationService
    {
        void NavigateTo(string viewName, object parameter = null);
        void NavigateToMainViewBasedOnRole(Employee employee, INavigationService navigationService);

        void CloseAllExcept(string viewName);

        void ReturnToMain(Employee employee, INavigationService navigationService);

        void NavigateToCreateExportStatistics(INavigationService navigationService);

        void NavigateToShowInsurances(INavigationService navigationService);

        void NavigateToShowCustomers(INavigationService navigationService);

        void NavigateToEditDeleteCustomer(INavigationService navigationService);

        void NavigateToEditDeleteInsurance(INavigationService navigationService);

        void NavigateToRegisterNewCustomer(INavigationService navigationService);

        void NavigateToRegisterNewInsurance(INavigationService navigationService);

        void NavigateToShowCustomerProspects(INavigationService navigationService);
    }
}
