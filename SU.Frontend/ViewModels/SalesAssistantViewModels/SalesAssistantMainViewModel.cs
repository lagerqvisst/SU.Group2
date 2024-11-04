using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using SU.Frontend.Views.SalesAssistantView;
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
        // Commands
        public ICommand ToRegisterNewCustomer { get; set; }
        public ICommand ToRegisterNewInsurance { get; set; }
        public ICommand ToEditDeleteCustomer { get; set; }
        public ICommand ToEditDeleteInsurance { get; set; }
        public ICommand ToShowInsurances { get; set; }
        public ICommand ToShowCustomers { get; set; }
        public ICommand ToShowCustomerProspects { get; set; }

        public ICommand ToRegisterNewSeller { get; set; }
        public ICommand ToEditDeleteSeller { get; set; }

        // Service
        public INavigationService _navigationService;

        // Constructor
        public SalesAssistantMainViewModel(INavigationService navigationService)
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

            ToRegisterNewSeller = new RelayCommand(() => NavigateToRegisterNewSeller());
            ToEditDeleteSeller = new RelayCommand(() => NavigateToEditDeleteSeller());

        }

        //Methods for navigating to unique views
        public void NavigateToRegisterNewSeller()
        {
            _navigationService.NavigateTo("RegisterNewSellerView", "SalesAssistantView");
        }

        public void NavigateToEditDeleteSeller()
        {
            _navigationService.NavigateTo("EditDeleteSellerView", "SalesAssistantView");
        }
    }
}