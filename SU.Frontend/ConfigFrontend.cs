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
using SU.Frontend.Views.CommonViews;
using SU.Frontend.Views.CommonViews.NewCustomer;
using SU.Frontend.ViewModels.CommonViewModels.Customers;
using SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated;
using SU.Frontend.Views.CommonViews.NewInsurance;
using SU.Frontend.ViewModels.CommonViewModels.NewInsurance;
using Microsoft.Extensions.Logging;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.ViewModels.CommonViewModels.CustomerRelated;
using SU.Frontend.ViewModels.CommonViewModels;

namespace SU.Frontend
{
    public static class ConfigFrontend
    {
        public static void AddFrontendServices(this IServiceCollection services)
        {
            // Registrera alla ViewModels
            services.AddTransient<TaskbarViewModel>();
            services.AddSingleton<LoginViewModel>(); // Registrerar LoginViewModel som singleton
            services.AddTransient<LogoutButtonViewModel>(); // ViewModel for Log Out button
            services.AddTransient<MainViewButtonViewModel>(); //ViewModel for MainView button
            services.AddTransient<SignedInUserViewModel>(); // ViewModel for Signed in user
            services.AddTransient<TaskbarViewModel>(); // View for TaskbarView
            services.AddTransient<RegisterNewCustomerViewModel>();
            services.AddTransient<NewPrivateCustomerViewModel>();
            services.AddTransient<NewCompanyCustomerViewModel>();
            services.AddTransient<RegisterNewInsuranceViewModel>();
            services.AddTransient<NewPrivateInsuranceViewModel>();
            services.AddTransient<NewCompanyInsuranceViewModel>();
            services.AddTransient<PrivateInsuranceTypeViewModel>();

            services.AddTransient<ShowCustomerViewModel>();
            services.AddTransient<ShowInsuranceViewModel>();
            services.AddTransient<EditDeleteCustomerViewModel>();
            services.AddTransient<EditDeleteInsuranceViewModel>();
            services.AddTransient<CreateSellStatViewModel>();
            services.AddTransient<CompanyInsuranceTypeViewModel>();
            services.AddTransient<ShowCustomerProspectViewModel>();

            //MainView ViewModels
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
            services.AddTransient<Taskbar>(); // UserControl for TaskbarView

            // Main Views
            services.AddTransient<CeoMainView>(); // View for CeoMainView
            services.AddTransient<FinancialAssistantMainView>();
            services.AddTransient<SalesAssistantMainView>(); // View for SalesAssistantMainView
            services.AddTransient<SalesManagerMainView>(); // View for SalesManagerMainView
            services.AddTransient<SellerMainView>(); // View for SellerMainView

            // Common Views
            services.AddTransient<CreateSellStatView>();
            services.AddTransient<EditDeleteCustomerView>();
            services.AddTransient<EditDeleteInsuranceView>();
            services.AddTransient<ShowCustomerProspectView>();
            

            //Register New Customer Views
            services.AddTransient<RegisterNewCustomerView>();
            services.AddTransient<NewPrivateCustomerView>();
            services.AddTransient<NewCompanyCustomerView>();

            //Register New Insurance Views
            services.AddTransient<RegisterNewInsuranceView>();
            services.AddTransient<NewPrivateInsuranceView>();
            services.AddTransient<NewCompanyInsuranceView>();
            services.AddTransient<PrivateCoverageTypeOptionView>();
            services.AddTransient<CompanyInsuranceTypeView>();
            services.AddTransient<ShowCustomerProspectView>();
            services.AddTransient<ShowCustomersView>();
            services.AddTransient<ShowInsurancesView>();


            //Specific Views
            //Financial Assistant
            services.AddTransient<RegisterExportBillingInfoView>();
            services.AddTransient<RegisterProvisionSellerView>();
            services.AddTransient<ShowSellingStatsView>();
            //SalesAssistant
            services.AddTransient<RegisterNewSellerView>();
            services.AddTransient<EditDeleteSellerView>();

            // Registrera andra tjänster
            services.AddScoped<INavigationService, NavigationService>();

            //Singletons
            services.AddSingleton<ILoggedInUserService, LoggedInUserService>();
            services.AddSingleton<IPolicyHolderService, PolicyHolderService>();
            services.AddSingleton<IInsuranceTypeService, InsuranceTypeService>();   

            services.AddTransient<IAuthenticationService, AuthenticationService>();


            // Loggning
            services.AddLogging(configure => configure.AddConsole());
        }
    }
}

