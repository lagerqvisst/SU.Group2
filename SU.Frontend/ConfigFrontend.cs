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

            // Registrera Views och UserControls
            services.AddTransient<LoginWindow>();
            services.AddTransient<TestView>();
            services.AddTransient<LogoutButtonControl>(); // UserControl for Log Out button
            services.AddTransient<MainViewButtonControl>(); // UserControl for MainView button

            // Registrera andra tjänster
            services.AddScoped<INavigationService, NavigationService>();
            services.AddSingleton<ILoggedInUserService, LoggedInUserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }
    }
}

