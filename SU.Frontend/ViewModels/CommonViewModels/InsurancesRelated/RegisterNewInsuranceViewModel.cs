using SU.Backend.Models.Enums.Insurance;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated
{
    public class RegisterNewInsuranceViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        // ICommand for Private Insurance Button
        public ICommand ToNewPrivateInsuranceCommand { get; }

        // ICommand for Company Insurance Button
        public ICommand ToNewCompanyInsuranceCommand { get; }

        public RegisterNewInsuranceViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            // Assign commands
            ToNewPrivateInsuranceCommand = new RelayCommand(NavigateToPrivateInsurance);
            ToNewCompanyInsuranceCommand = new RelayCommand(NavigateToCompanyInsurance);
        }

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
}
