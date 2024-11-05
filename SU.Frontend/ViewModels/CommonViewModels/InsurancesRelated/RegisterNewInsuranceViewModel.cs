using System.Windows.Input;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated;

public class RegisterNewInsuranceViewModel : ObservableObject
{
    // Service
    private readonly INavigationService _navigationService;

    public RegisterNewInsuranceViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        // Assign commands
        ToNewPrivateInsuranceCommand = new RelayCommand(NavigateToPrivateInsurance);
        ToNewCompanyInsuranceCommand = new RelayCommand(NavigateToCompanyInsurance);
    }

    // ICommand for Private Insurance Button
    public ICommand ToNewPrivateInsuranceCommand { get; }

    // ICommand for Company Insurance Button
    public ICommand ToNewCompanyInsuranceCommand { get; }

    // Navigation logic for Private Insurance
    private void NavigateToPrivateInsurance()
    {
        _navigationService.NavigateToNewPrivateInsurance();
    }

    // Navigation logic for Company Insurance
    private void NavigateToCompanyInsurance()
    {
        _navigationService.NavigateToNewCompanyInsurance();
    }
}