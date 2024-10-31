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
        // Lägg till register billing, reg provision, show selling stats

        public ICommand ToShowInsurances { get; set; }
        public ICommand ToShowCustomers { get; set; }

        public ICommand ToRegisterBillingInfo { get; set; }

        public ICommand ToRegisterProvision { get; set; }

        public ICommand ToShowSellingStat { get; set; }

        public INavigationService _navigationService;

        public FinancialAssistantMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances());
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers());
            ToRegisterBillingInfo = new RelayCommand(() => NavigateToShowBillingInfo());
            ToRegisterProvision = new RelayCommand(() => NavigateToShowProvision());
            ToShowSellingStat = new RelayCommand(() => NavigateToShowSellingStat());
        }

        public void NavigateToShowBillingInfo()
        {
            _navigationService.NavigateTo("InvoiceView", "FinancialAssistantView");
        }

        public void NavigateToShowProvision()
        {
            _navigationService.NavigateTo("ComissionView", "FinancialAssistantView");
        }

        public void NavigateToShowSellingStat()
        {
            _navigationService.NavigateTo("ShowSellingStatsView", "FinancialAssistantView");
        }
    }
}
