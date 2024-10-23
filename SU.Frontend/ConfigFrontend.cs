using Microsoft.Extensions.DependencyInjection;
using SU.Backend.Controllers;
using SU.Backend.Database;
using SU.Backend.Services.Interfaces;
using SU.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Frontend.Helper.Navigation;
using SU.Frontend.Helper.User;
using SU.Frontend.ViewModels;
using SU.Frontend.Views;
using SU.Frontend.Helper.Authentication;
using SU.Frontend.ViewModels.UserControlViewModels;
using SU.Frontend.Views.UserControls;
using SU.Frontend.ViewModels.FinancialAssistantViewModels;
using SU.Frontend.ViewModels.CeoMainViewModel;
using SU.Frontend.ViewModels.SalesAssistantViewModels;
using SU.Frontend.ViewModels.SellerViewModels;
using SU.Frontend.ViewModels.SalesManagerViewModels;
using SU.Frontend.Views.CeoView;
using SU.Frontend.Views.FinancialAssistantView;
using SU.Frontend.Views.SalesAssistantView;
using SU.Frontend.Views.SalesManagerView;
using SU.Frontend.Views.SellerView;

namespace SU.Frontend
{
    public static class ConfigFrontend
    {
        public static void AddFrontendServices(this IServiceCollection services)
        {
            // Registrera alla ViewModels
            services.AddTransient<TaskbarViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<LogoutButtonViewModel>(); // ViewModel for Log Out button
            services.AddTransient<MainViewButtonViewModel>(); //ViewModel for MainView button
            services.AddTransient<SignedInUserViewModel>(); // ViewModel for Signed in user
            services.AddTransient<EditDeleteCustomerButtonViewModel>(); // ViewModel for EditDeleteCustomerButton
            services.AddTransient<EditDeleteInsuranceButtonViewModel>(); // ViewModel for EditDeleteInsuranceButton
            services.AddTransient<EditDeleteSellerButtonViewModel>(); // ViewModel for EditDeleteSellerButton
            services.AddTransient<RegisterNewCustomerButtonViewModel>(); // ViewModel for RegisterNewCustomerButton
            services.AddTransient<RegisterNewInsuranceButtonViewModel>(); // ViewModel for RegisterNewInsuranceButton
            services.AddTransient<RegisterNewSellerButtonViewModel>(); // ViewModel for RegisterNewSellerButton
            services.AddTransient<RegShowExportBillingInfoButtonViewModel>(); // ViewModel for RegShowExportBillingInfoButton
            services.AddTransient<RegShowProvisionForSellerButtonViewModel>(); // ViewModel for RegShowProvisionForSellerButton
            services.AddTransient<ShowCreateExportSellStatButtonViewModel>(); // ViewModel for ShowCreateExportSellStatButton
            services.AddTransient<ShowCustomerProspectButtonViewModel>(); // ViewModel for ShowCustomerProspectButton
            services.AddTransient<ShowSearchCustomerButtonViewModel>(); // ViewModel for ShowSearchCustomerButton
            services.AddTransient<ShowSearchInsuranceButtonViewModel>(); // ViewModel for ShowSearchInsuranceButton
            services.AddTransient<ShowSellingStatButtonViewModel>(); // ViewModel for ShowSellingStatButton

            services.AddTransient<CeoMainViewModel>(); // ViewModel for CeoMainView
            services.AddTransient<FinancialAssistantMainViewModel>(); // ViewModel for FinancialAssistantMainView
            services.AddTransient<SalesAssistantMainViewModel>(); // ViewModel for SalesAssistantMainView
            services.AddTransient<SalesManagerMainViewModel>(); // ViewModel for SalesManagerMainView
            services.AddTransient<SellerMainViewModel>(); // ViewModel for SellerMainView


            // Registrera Views och UserControls
            services.AddTransient<LoginWindow>();
            services.AddTransient<TestView>();
            services.AddTransient<LogoutButtonControl>(); // UserControl for Log Out button
            services.AddTransient<MainViewButtonControl>(); // UserControl for MainView button
            services.AddTransient<SignedInUserUserControl>(); // UserControl for Signed in user
            services.AddTransient<EditDeleteCustomerButtonControl>(); // UserControl for EditDeleteCustomerButton
            services.AddTransient<EditDeleteInsuranceButtonControl>(); // UserControl for EditDeleteInsuranceButton
            services.AddTransient<EditDeleteSellerButtonControl>(); // UserControl for EditDeleteSellerButton
            services.AddTransient<RegisterNewCustomerButtonControl>(); // UserControl for RegisterNewCustomerButton
            services.AddTransient<RegisterNewInsuranceButtonControl>(); // UserControl for RegisterNewInsuranceButton
            services.AddTransient<RegisterNewSellerButtonControl>(); // UserControl for RegisterNewSellerButton
            services.AddTransient<RegShowExportBillingInfoButtonControl>(); // UserControl for RegShowExportBillingInfoButton
            services.AddTransient<RegShowProvisionForSellerButtonControl>(); // UserControl for RegShowProvisionForSellerButton
            services.AddTransient<ShowCreateExportSellStatButtonControl>(); // UserControl for ShowCreateExportSellStatButton
            services.AddTransient<ShowCustomerProspectButtonControl>(); // UserControl for ShowCustomerProspectButton
            services.AddTransient<ShowSearchCustomerButtonControl>(); // UserControl for ShowSearchCustomerButton
            services.AddTransient<ShowSearchInsuranceButtonControl>(); // UserControl for ShowSearchInsuranceButton
            services.AddTransient<ShowSellingStatButtonControl>(); // UserControl for ShowSellingStatButton

            services.AddTransient<CeoMainView>(); // View for CeoMainView
            services.AddTransient<FinancialAssistantMainView>();
            services.AddTransient<SalesAssistantMainView>(); // View for SalesAssistantMainView
            services.AddTransient<SalesManagerMainView>(); // View for SalesManagerMainView
            services.AddTransient<SellerMainView>(); // View for SellerMainView

            // Registrera andra tjänster
            services.AddScoped<INavigationService, NavigationService>();
            services.AddSingleton<ILoggedInUserService, LoggedInUserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }
    }
}

