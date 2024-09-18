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
    }
}
