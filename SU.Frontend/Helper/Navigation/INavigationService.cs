﻿using SU.Backend.Models.Employees;

namespace SU.Frontend.Helper.Navigation;

public interface INavigationService
{
    void NavigateTo(string viewName, string folderName, object parameter = null);
    void NavigateToMainViewBasedOnRole(Employee employee);

    void CloseAllExcept(string viewName, bool minimizeLoginWindow = true);
    void ReturnToMain(Employee employee);

    void ReturnToPrevious();

    void NavigateToMonthlyStatistics();

    void NavigateToTrends();

    void NavigateToShowInsurances();

    void NavigateToShowCustomers();

    void NavigateToEditDeleteCustomer();

    void NavigateToEditDeleteInsurance();

    void NavigateToRegisterNewCustomer();

    void NavigateToRegisterNewInsurance();

    void NavigateToShowCustomerProspects();

    void NavigateToNewPrivateCustomer();

    void NavigateToNewCompanyCustomer();

    void NavigateToNewPrivateInsurance();

    void NavigateToNewCompanyInsurance();

    void NavigateToNewOutsideSeller();

    void NavigateToNewInsideSeller();
}