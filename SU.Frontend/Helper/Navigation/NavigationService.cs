using System.Windows;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;

namespace SU.Frontend.Helper.Navigation;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    // Constructor that takes an IServiceProvider as a parameter to resolve views from the DI container
    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    // Method to close all windows except the one with the specified view name
    public void CloseAllExcept(string viewName)
    {
        foreach (var window in Application.Current.Windows.OfType<Window>())
            if (window.GetType().Name != viewName)
                window.Close();
    }

    // Method to navigate to the main view based on the employee's role
    public void NavigateToMainViewBasedOnRole(Employee employee)
    {
        Window existingWindow = null;

        if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.CEO))
        {
            existingWindow = GetExistingWindow("CeoMainView");
            if (existingWindow == null)
                NavigateTo("CeoMainView", "CeoView");
        }
        else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.SalesAssistant))
        {
            existingWindow = GetExistingWindow("SalesAssistantMainView");
            if (existingWindow == null)
                NavigateTo("SalesAssistantMainView", "SalesAssistantView");
        }
        else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.SalesManager))
        {
            existingWindow = GetExistingWindow("SalesManagerMainView");
            if (existingWindow == null)
                NavigateTo("SalesManagerMainView", "SalesManagerView");
        }
        else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.OutsideSales))
        {
            existingWindow = GetExistingWindow("SellerMainView");
            if (existingWindow == null)
                NavigateTo("SellerMainView", "SellerView");
        }
        else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.InsideSales))
        {
            existingWindow = GetExistingWindow("SellerMainView");
            if (existingWindow == null)
                NavigateTo("SellerMainView", "SellerView");
        }
        else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.FinancialAssistant))
        {
            existingWindow = GetExistingWindow("FinancialAssistantMainView");
            if (existingWindow == null)
                NavigateTo("FinancialAssistantMainView", "FinancialAssistantView");
        }
        else
        {
            existingWindow = GetExistingWindow("DefaultDashboardView");
            if (existingWindow == null)
                NavigateTo("DefaultDashboardView");
        }

        if (existingWindow != null)
        {
            existingWindow.Activate();
            existingWindow.Show();
        }
    }

    // Method to navigate to a specific view based on the view name, folder name, and optional parameter
    public void NavigateTo(string viewName, string folderName = null, object parameter = null)
    {
        if (string.IsNullOrEmpty(viewName))
            throw new ArgumentNullException(nameof(viewName), "View name cannot be null or empty.");

        // If a folder is passed in, include it in the full type name
        var fullViewName = string.IsNullOrEmpty(folderName)
            ? $"SU.Frontend.Views.{viewName}"
            : $"SU.Frontend.Views.{folderName}.{viewName}";

        // Get the type based on the full name
        var viewType = Type.GetType(fullViewName);
        if (viewType == null) throw new ArgumentException($"View type '{fullViewName}' not found.", nameof(viewName));

        // Get the view from the DI container
        var view = (Window)_serviceProvider.GetService(viewType);
        if (view != null)
        {
            // If a parameter is passed in, set the DataContext of the view
            if (parameter != null) view.DataContext = parameter;

            // Show the view
            view.Show();
        }
        else
        {
            throw new Exception($"View '{fullViewName}' could not be resolved.");
        }
    }

    // Method to return to the previous window
    public void ReturnToPrevious()
    {
        var currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);

        if (currentWindow != null) currentWindow.Close();
    }

    // Method to return to the main view based on the employee's role
    public void ReturnToMain(Employee employee)
    {
        // Navigate to the main view based on the role
        NavigateToMainViewBasedOnRole(employee);

        // Wait for the UI thread to finish navigation before closing windows
        Application.Current.Dispatcher.InvokeAsync(() =>
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);

            if (currentWindow != null)
            {
                var windowName = currentWindow.GetType().Name; // Use the window type name instead of Title
                CloseAllExcept(windowName);

                // Log or show current window name
                Console.WriteLine($"Current window: {windowName}");
            }
            else
            {
                Console.WriteLine("No active window found.");
            }
        });
    }

    // Method to get an existing window based on the view name
    private Window GetExistingWindow(string viewName)
    {
        return Application.Current.Windows.OfType<Window>()
            .FirstOrDefault(window => window.GetType().Name == viewName);
    }

    #region Navigation Methods

    public void NavigateToMonthlyStatistics()
    {
        NavigateTo("MonthlyStatisticsView", "CommonViews.Statistics");
    }

    public void NavigateToShowInsurances()
    {
        NavigateTo("ShowInsurancesView", "CommonViews");
    }

    public void NavigateToShowCustomers()
    {
        NavigateTo("ShowCustomersView", "CommonViews");
    }

    public void NavigateToEditDeleteCustomer()
    {
        NavigateTo("EditDeleteCustomerView", "CommonViews");
    }

    public void NavigateToEditDeleteInsurance()
    {
        NavigateTo("EditDeleteInsuranceView", "CommonViews");
    }

    public void NavigateToRegisterNewCustomer()
    {
        NavigateTo("RegisterNewCustomerView", "CommonViews");
    }

    public void NavigateToRegisterNewInsurance()
    {
        NavigateTo("RegisterNewInsuranceView", "CommonViews");
    }

    public void NavigateToShowCustomerProspects()
    {
        NavigateTo("ShowCustomerProspectView", "CommonViews");
    }

    public void NavigateToNewPrivateCustomer()
    {
        NavigateTo("NewPrivateCustomerView", "CommonViews.NewCustomer");
    }

    public void NavigateToNewCompanyCustomer()
    {
        NavigateTo("NewCompanyCustomerView", "CommonViews.NewCustomer");
    }

    public void NavigateToNewPrivateInsurance()
    {
        NavigateTo("NewPrivateInsuranceView", "CommonViews.NewInsurance");
    }

    public void NavigateToNewCompanyInsurance()
    {
        NavigateTo("NewCompanyInsuranceView", "CommonViews.NewInsurance");
    }

    public void NavigateToNewOutsideSeller()
    {
        NavigateTo("NewOutsideSellerView", "SalesAssistantViews.RegisterNewSeller");
    }

    public void NavigateToNewInsideSeller()
    {
        NavigateTo("NewInsideSellerView", "SalesAssistantViews.RegisterNewSeller");
    }

    public void NavigateToTrends()
    {
        NavigateTo("TrendsView", "CommonViews.Statistics");
    }

    #endregion Navigation Methods
}