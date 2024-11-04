   using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.CommonViewModels.Customers
{
    public class RegisterNewCustomerViewModel : ObservableObject
    {
        INavigationService _navigationService;

        // Commands to public properties
        public ICommand ToNewRegisterPrivateCommand { get; set; }
        public ICommand ToNewRegisterCompanyCommand { get; set; }

        // Constructor
        public RegisterNewCustomerViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToNewRegisterPrivateCommand = new RelayCommand(() => _navigationService.NavigateToNewPrivateCustomer());
            ToNewRegisterCompanyCommand = new RelayCommand(() => _navigationService.NavigateToNewCompanyCustomer());
        }
    }
}
