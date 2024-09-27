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
using SU.Frontend.ViewModels.UserControls;
using SU.Frontend.ViewModels;
using SU.Frontend.Views.UserControls;
using SU.Frontend.Views;

namespace SU.Frontend
{
    public static class ConfigFrontend
    {
        public static void AddFrontendServices(this IServiceCollection services)
        {
            // Registrera tjänster och deras beroenden här

            // Registrera både LoginWindow och LoginViewModel
            services.AddTransient<TaskbarViewModel>();
            services.AddTransient<TaskbarView>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginWindow>();
            services.AddTransient<StatisticsViewModel>();
            services.AddTransient<Statistics>();

            //Frontend services
            services.AddScoped<INavigationService, NavigationService>();
            services.AddSingleton<ILoggedInUserService, LoggedInUserService>();
            


        }
    }
}
