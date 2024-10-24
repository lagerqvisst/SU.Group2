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
        public INavigationService _navigationService;

        public FinancialAssistantMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances());
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers());
        }

        public void ToShowBillingInfo()
        {
            _navigationService.NavigateTo("RegisterExportBillingInfoView", "FinancialAssistantView");
        }

        public void ToShowProvision()
        {
            _navigationService.NavigateTo("RegisterProvisionSellerView", "FinancialAssistantView");
        }

        public void ToShowSellingStat()
        {
            _navigationService.NavigateTo("ShowSellingStatisticsView", "FinancialAssistantView");
        }
    }
}
