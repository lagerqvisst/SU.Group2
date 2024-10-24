using Microsoft.Extensions.DependencyInjection;
using SU.Frontend.ViewModels.CeoMainViewModel;
using SU.Frontend.ViewModels.FinancialAssistantViewModels;
using SU.Frontend.ViewModels.SalesAssistantViewModels;
using SU.Frontend.ViewModels.SalesManagerViewModels;
using SU.Frontend.ViewModels.SellerViewModels;
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

            public SignedInUserViewModel SignedInUserViewModel =>
                App.AppHost?.Services.GetService<SignedInUserViewModel>();

           public CeoMainViewModel CeoMainViewModel =>
                App.AppHost?.Services.GetService<CeoMainViewModel>();

            public FinancialAssistantMainViewModel FinancialAssistantMainViewModel =>
                App.AppHost?.Services.GetService<FinancialAssistantMainViewModel>();

            public SalesManagerMainViewModel SalesManagerMainViewModel =>
                App.AppHost?.Services.GetService<SalesManagerMainViewModel>();

            public SellerMainViewModel SellerMainViewModel => 
                App.AppHost?.Services.GetService<SellerMainViewModel>();

            public SalesAssistantMainViewModel SalesAssistantMainViewModel =>
                App.AppHost?.Services.GetService<SalesAssistantMainViewModel>();

            public TaskbarViewModel TaskbarViewModel =>
                App.AppHost?.Services.GetService<TaskbarViewModel>();
        }
    }



}
