using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void NavigateBasedOnRole(Employee employee, INavigationService navigationService)
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
                navigationService.NavigateTo("OutsideSalesView");
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
            // Logic to resolve view and show it
            var view = (Window)_serviceProvider.GetService(Type.GetType(viewName));
            if (view != null)
            {
                view.DataContext = parameter;
                view.Show();
            }
        }
    }
}
