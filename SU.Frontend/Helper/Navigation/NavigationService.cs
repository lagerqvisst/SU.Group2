using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Frontend.Views.CommonViews;
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


        private Window GetExistingWindow(string viewName)
        {
            return Application.Current.Windows.OfType<Window>()
                   .FirstOrDefault(window => window.GetType().Name == viewName);
        }


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
                    NavigateTo("DefaultDashboardView", null);
            }

            if (existingWindow != null)
            {
                existingWindow.Activate();
                existingWindow.Show();
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

        public void ReturnToPrevious()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);

            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }


        public void ReturnToMain(Employee employee)
        {
            // Navigate to the main view based on the role
            NavigateToMainViewBasedOnRole(employee);

            // Wait for the UI thread to finish navigation before closing windows
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);

                if (currentWindow != null)
                {
                    string windowName = currentWindow.GetType().Name; // Use the window type name instead of Title
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


        public void NavigateToMonthlyStatistics()
        {
            NavigateTo("MonthlyStatisticsView", "CommonViews.Statistics");
        }

        public void NavigateToShowInsurances()
        {
            NavigateTo("ShowInsurancesView", "CommonViews");
        }

        public void NavigateToShowCustomers ()
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
    }
}
