using SU.Frontend.Helper.Authentication;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SU.Frontend.Helper.Navigation;
using SU.Backend.Models.Employees;
using SU.Frontend.Helper.DI_Objects.User;

namespace SU.Frontend.ViewModels.UserControlViewModels
{
    public class MainViewButtonViewModel
    {

        public ICommand ReturnToMainViewCommand { get; }
        public ILoggedInUserService _loggedInUserService { get; }
        public INavigationService _navigationService { get; }

        public MainViewButtonViewModel(INavigationService navigationService, ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
            _navigationService = navigationService;

            ReturnToMainViewCommand = new RelayCommand(ReturnToMainView);
        }

        public void ReturnToMainView()
        {
            _navigationService.ReturnToMain(_loggedInUserService.LoggedInEmployee);
        }
    }
}
