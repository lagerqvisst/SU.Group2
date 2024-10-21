using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                navigationService.NavigateTo("CEODashboardView");
            }
            else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.SalesManager))
            {
                navigationService.NavigateTo("SalesManagerView");
            }
            else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.OutsideSales))
            {
                navigationService.NavigateTo("TestView");
            }
            else if (employee.RoleAssignments.Any(r => r.Role == EmployeeType.InsideSales))
            {
                navigationService.NavigateTo("InsideSalesView");
            }
            else
            {
                navigationService.NavigateTo("DefaultDashboardView");
            }
        }

        public void NavigateTo(string viewName, object parameter = null)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException(nameof(viewName), "View name cannot be null or empty.");
            }

            // Hämta typen baserat på dess fullständiga namn
            var viewType = Type.GetType($"SU.Frontend.Views.{viewName}");
            if (viewType == null)
            {
                throw new ArgumentException($"View type '{viewName}' not found.", nameof(viewName));
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
                throw new Exception($"View '{viewName}' could not be resolved.");
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
    }
}
