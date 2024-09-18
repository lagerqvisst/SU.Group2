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
