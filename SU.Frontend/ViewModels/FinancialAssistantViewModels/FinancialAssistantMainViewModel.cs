using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.FinancialAssistantViewModels
{
    public class FinancialAssistantMainViewModel : ObservableObject
    {
        // Commands
        public ICommand ToShowInsurances { get; set; }
        public ICommand ToShowCustomers { get; set; }
        public ICommand ToRegisterBillingInfo { get; set; }
        public ICommand ToRegisterProvision { get; set; }
        public ICommand ToShowSellingStat { get; set; }

        // Service
        public INavigationService _navigationService;

        // Constructor
        public FinancialAssistantMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            //Shared views are navigated through the navigation service
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances());
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers());
            ToRegisterBillingInfo = new RelayCommand(() => NavigateToInvoices());
            ToRegisterProvision = new RelayCommand(() => NavigateToShowProvision());
            ToShowSellingStat = new RelayCommand(() => _navigationService.NavigateToMonthlyStatistics());
        }

        // Navigation logic
        public void NavigateToInvoices()
        {
            _navigationService.NavigateTo("InvoiceView", "FinancialAssistantView");
        }

        public void NavigateToShowProvision()
        {
            _navigationService.NavigateTo("ComissionView", "FinancialAssistantView");
        }
    }
}