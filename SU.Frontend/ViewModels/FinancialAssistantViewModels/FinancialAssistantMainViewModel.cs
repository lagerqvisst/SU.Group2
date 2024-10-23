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

        public ICommand ToShowInsurances;
        public ICommand ToShowCustomers;
        public INavigationService _navigationService;

        public FinancialAssistantMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances(_navigationService));
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers(_navigationService));
        }

        public void RegisterExportBillingInfo()
        {
            _navigationService.NavigateTo("RegisterExportBillingInfoView");
        }

        public void RegisterProvisionSeller()
        {
            _navigationService.NavigateTo("RegisterProvisionSellerView");
        }

        public void ShowSellingStatistics()
        {
            _navigationService.NavigateTo("ShowSellingStatisticsView");
        }
    }
}
