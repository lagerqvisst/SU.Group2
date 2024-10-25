using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.CommonViewModels
{
    public class RegisterNewCustomerViewModel : ObservableObject
    {
        INavigationService _navigationService;

        public ICommand ToNewRegisterPrivateCommand { get; set; }
        public ICommand ToNewRegisterCompanyCommand { get; set; }

        public RegisterNewCustomerViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToNewRegisterPrivateCommand = new RelayCommand(() => _navigationService.NavigateToNewPrivateCustomer());
            ToNewRegisterCompanyCommand = new RelayCommand(() => _navigationService.NavigateToNewCompanyCustomer());
        }
    }
}
