using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace SU.Frontend.Helper.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CloseAllExcept(string viewName)
        {
            foreach (var window in Application.Current.Windows.OfType<Window>())
            {
                if (window.GetType().Name != viewName)
                {
                    window.Close();
                }
            }
        }

        public void NavigateToMainViewBasedOnRole(Employee employee, INavigationService navigationService)
        {
            if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.CEO))
            {
                navigationService.NavigateTo("CeoMainView", "CeoView");
            }
            else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.SalesAssistant))
            {
                navigationService.NavigateTo("SalesAssistantMainView", "SalesAssistantView");
            }
            else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.SalesManager))
            {
                navigationService.NavigateTo("SalesManagerMainView", "SalesManagerView");
            }
            else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.OutsideSales))
            {
                navigationService.NavigateTo("TestView", null);
            }
            else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.InsideSales))
            {
                navigationService.NavigateTo("SellerMainView", "SellerView");
            }
            else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.FinancialAssistant))
            {
                navigationService.NavigateTo("FinancialAssistantMainView", "FinancialAssistantView");
            }
            else
            {
                navigationService.NavigateTo("DefaultDashboardView", null);
            }
        }

        public void NavigateTo(string viewName, string folderName = null, object parameter = null)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName), "View name cannot be null or empty.");
            }

            // Om en mapp skickas in, inkludera den i typens fullständiga namn
            var fullViewName = string.IsNullOrEmpty(folderName)
                ? $"SU.Frontend.Views.{viewName}"
                : $"SU.Frontend.Views.{folderName}.{viewName}";

            // Hämta typen baserat på det fullständiga namnet
            var viewType = Type.GetType(fullViewName);
            if (viewType == null)
            {
                throw new ArgumentException($"View type '{fullViewName}' not found.", nameof(viewName));
            }

            // Hämta vyn från DI-behållaren
            var view = (Window)_serviceProvider.GetService(viewType);
            if (view != null)
            {
                // Om parameter skickas med, sätt DataContext till det
                if (parameter != null)
                {
                    view.DataContext = parameter;
                }

                // Visa vyn
                view.Show();
            }
            else
            {
                throw new Exception($"View '{fullViewName}' could not be resolved.");
            }
        }


        public void ReturnToMain(Employee employee, INavigationService navigationService)
        {
            NavigateToMainViewBasedOnRole(employee, navigationService);

            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            // Check if window is current
            if (currentWindow != null)
            {
                // Map current window to string 
                string windowName = currentWindow.Title;

                CloseAllExcept(windowName);

                // Show window as string 
                Console.WriteLine($"Current window: {windowName}");
            }
            else
            {
                Console.WriteLine("No active window found.");
            }
        }

        public void NavigateToCreateExportStatistics(INavigationService navigationService)
        {
            navigationService.NavigateTo("CreateExportSellStatView", "CommonViews");
        }

        public void NavigateToShowInsurances(INavigationService navigationService)
        {
            navigationService.NavigateTo("ShowInsurancesView", "CommonViews");
        }

        public void NavigateToShowCustomers (INavigationService navigationService)
        {
            navigationService.NavigateTo("ShowCustomersView", "CommonViews");
        }

        public void NavigateToEditDeleteCustomer(INavigationService navigationService)
        {
            navigationService.NavigateTo("EditDeleteCustomerView", "CommonViews");
        }

        public void NavigateToEditDeleteInsurance(INavigationService navigationService)
        {
            navigationService.NavigateTo("EditDeleteInsuranceView", "CommonViews");
        }

        public void NavigateToRegisterNewCustomer(INavigationService navigationService)
        {
            navigationService.NavigateTo("RegisterNewCustomerView", "CommonViews");
        }

        public void NavigateToRegisterNewInsurance(INavigationService navigationService)
        {
            navigationService.NavigateTo("RegisterNewInsuranceView", "CommonViews");
        }

        public void NavigateToShowCustomerProspects(INavigationService navigationService)
        {
            navigationService.NavigateTo("ShowCustomerProspectsView", "CommonViews");
        }
    }
}
