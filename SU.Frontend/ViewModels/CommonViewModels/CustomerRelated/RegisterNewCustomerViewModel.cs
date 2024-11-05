using System.Windows.Input;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.CommonViewModels.Customers;

public class RegisterNewCustomerViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    // Constructor
    public RegisterNewCustomerViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        ToNewRegisterPrivateCommand = new RelayCommand(() => _navigationService.NavigateToNewPrivateCustomer());
        ToNewRegisterCompanyCommand = new RelayCommand(() => _navigationService.NavigateToNewCompanyCustomer());
    }

    // Commands to public properties
    public ICommand ToNewRegisterPrivateCommand { get; set; }
    public ICommand ToNewRegisterCompanyCommand { get; set; }
}