using System.Windows.Input;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.UserControlViewModels;

public class ReturnButtonViewModel
{
    // Constructor
    public ReturnButtonViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        ReturnCommand = new RelayCommand(ReturnToPrevious);
    }

    // Command
    public ICommand ReturnCommand { get; }

    // Service
    public INavigationService _navigationService { get; }

    // Method to return to the previous view
    public void ReturnToPrevious()
    {
        _navigationService.ReturnToPrevious();
    }
}