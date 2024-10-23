using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.SellerViewModels
{
    public class SellerMainViewModel : ObservableObject
    {
        public ICommand ToRegisterNewCustomer;
        public ICommand ToRegisterNewInsurance;
        public ICommand ToEditDeleteCustomer;
        public ICommand ToEditDeleteInsurance;
        public ICommand ToShowInsurances;
        public ICommand ToShowCustomers;
        public ICommand ToShowCustomerProspects;
        public INavigationService _navigationService;
        public SellerMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //Shared views are navigated through the navigation service
            ToRegisterNewCustomer = new RelayCommand(() => _navigationService.NavigateToRegisterNewCustomer());
            ToRegisterNewInsurance = new RelayCommand(() => _navigationService.NavigateToRegisterNewInsurance());
            ToEditDeleteCustomer = new RelayCommand(() => _navigationService.NavigateToEditDeleteCustomer());
            ToEditDeleteInsurance = new RelayCommand(() => _navigationService.NavigateToEditDeleteInsurance());
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances());
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers());
            ToShowCustomerProspects = new RelayCommand(() => _navigationService.NavigateToShowCustomerProspects());
        }
    }
}
