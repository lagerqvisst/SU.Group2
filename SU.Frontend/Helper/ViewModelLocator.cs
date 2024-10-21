using Microsoft.Extensions.DependencyInjection;
using SU.Frontend.ViewModels.UserControlViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper
{
    namespace SU.Frontend.Helper
    {
        public class ViewModelLocator
        {
            public LogoutButtonViewModel LogoutButtonViewModel =>
                App.AppHost?.Services.GetService<LogoutButtonViewModel>();

            public MainViewButtonViewModel MainViewButtonViewModel =>
                App.AppHost?.Services.GetService<MainViewButtonViewModel>();
        }
    }



}
