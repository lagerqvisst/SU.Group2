using System.Windows.Input;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.UserControlViewModels;

public class MainViewButtonViewModel
{
    // Constructor
    public MainViewButtonViewModel(INavigationService navigationService, ILoggedInUserService loggedInUserService)
    {
        _loggedInUserService = loggedInUserService;
        _navigationService = navigationService;

        ReturnToMainViewCommand = new RelayCommand(ReturnToMainView);
    }

    // Command
    public ICommand ReturnToMainViewCommand { get; }

    // Services
    public ILoggedInUserService _loggedInUserService { get; }
    public INavigationService _navigationService { get; }

    // Method to return to the main view
    public void ReturnToMainView()
    {
        _navigationService.ReturnToMain(_loggedInUserService.LoggedInEmployee);
    }
}