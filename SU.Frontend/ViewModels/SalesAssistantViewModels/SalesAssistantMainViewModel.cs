using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.SalesAssistantViewModels
{
    public class SalesAssistantMainViewModel : ObservableObject
    {
        //Lägg till Register seller och edit/delete seller

        public ICommand ToRegisterNewCustomer;
        public ICommand ToRegisterNewInsurance;
        public ICommand ToEditDeleteCustomer;
        public ICommand ToEditDeleteInsurance;
        public ICommand ToShowInsurances;
        public ICommand ToShowCustomers;
        public ICommand ToShowCustomerProspects;
        public INavigationService _navigationService;
        public SalesAssistantMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToRegisterNewCustomer = new RelayCommand(() => _navigationService.NavigateToRegisterNewCustomer(_navigationService));
            ToRegisterNewInsurance = new RelayCommand(() => _navigationService.NavigateToRegisterNewInsurance(_navigationService));
            ToEditDeleteCustomer = new RelayCommand(() => _navigationService.NavigateToEditDeleteCustomer(_navigationService));
            ToEditDeleteInsurance = new RelayCommand(() => _navigationService.NavigateToEditDeleteInsurance(_navigationService));
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances(_navigationService));
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers(_navigationService));
            ToShowCustomerProspects = new RelayCommand(() => _navigationService.NavigateToShowCustomerProspects(_navigationService));
        }
    }
}