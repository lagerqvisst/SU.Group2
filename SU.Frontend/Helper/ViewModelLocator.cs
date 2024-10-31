using Microsoft.Extensions.DependencyInjection;
using SU.Frontend.ViewModels.CeoMainViewModel;
using SU.Frontend.ViewModels.CommonViewModels.CustomerRelated;
using SU.Frontend.ViewModels.CommonViewModels.Customers;
using SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated;
using SU.Frontend.ViewModels.CommonViewModels.NewInsurance;
using SU.Frontend.ViewModels.FinancialAssistantViewModels;
using SU.Frontend.ViewModels.SalesAssistantViewModels;
using SU.Frontend.ViewModels.SalesManagerViewModels;
using SU.Frontend.ViewModels.SellerViewModels;
using SU.Frontend.ViewModels.Statistics;
using SU.Frontend.ViewModels.UserControlViewModels;


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

            public RegisterNewCustomerViewModel RegisterNewCustomerViewModel =>
                App.AppHost?.Services.GetService<RegisterNewCustomerViewModel>();

            public NewPrivateCustomerViewModel NewPrivateCustomerViewModel =>
                App.AppHost?.Services.GetService<NewPrivateCustomerViewModel>();

            public NewCompanyCustomerViewModel NewCompanyCustomerViewModel =>
                App.AppHost?.Services.GetService<NewCompanyCustomerViewModel>();

            public RegisterNewInsuranceViewModel RegisterNewInsuranceViewModel =>
                App.AppHost?.Services.GetService<RegisterNewInsuranceViewModel>();

            public NewPrivateInsuranceViewModel NewPrivateInsuranceViewModel =>
                App.AppHost?.Services.GetService<NewPrivateInsuranceViewModel>();

            public PrivateInsuranceTypeViewModel PrivateInsuranceTypeViewModel =>
                App.AppHost?.Services.GetService<PrivateInsuranceTypeViewModel>();

            public NewCompanyInsuranceViewModel NewCompanyInsuranceViewModel =>
                App.AppHost?.Services.GetService<NewCompanyInsuranceViewModel>();

            public CompanyInsuranceTypeViewModel CompanyInsuranceTypeViewModel =>
                App.AppHost?.Services.GetService<CompanyInsuranceTypeViewModel>();

            public ShowCustomerViewModel ShowCustomerViewModel =>
                App.AppHost?.Services.GetService<ShowCustomerViewModel>();

            public ShowInsuranceViewModel ShowInsuranceViewModel =>
                App.AppHost?.Services.GetService<ShowInsuranceViewModel>();

           public EditDeleteCustomerViewModel EditDeleteCustomerViewModel =>
                App.AppHost?.Services.GetService<EditDeleteCustomerViewModel>();

            public EditDeleteSellerViewModel EditDeleteSellerViewModel =>
                App.AppHost?.Services.GetService<EditDeleteSellerViewModel>();

            public EditDeleteInsuranceViewModel EditDeleteInsuranceViewModel =>
              App.AppHost?.Services.GetService<EditDeleteInsuranceViewModel>();

            public MonthlyStatisticsViewModel CreateSellStatViewModel =>
                App.AppHost?.Services.GetService<MonthlyStatisticsViewModel>();

            public RegisterNewSellerViewModel RegisterNewSellerViewModel =>
                App.AppHost?.Services.GetService<RegisterNewSellerViewModel>();

            public CommissionViewModel CommissionViewModel =>
                App.AppHost?.Services.GetService<CommissionViewModel>();

            public DownloadButtonViewModel DownloadButtonViewModel =>
                App.AppHost?.Services.GetService<DownloadButtonViewModel>();
          
            public ShowCustomerProspectViewModel ShowCustomerProspectViewModel =>
                App.AppHost?.Services.GetService<ShowCustomerProspectViewModel>();

        }
    }

}
