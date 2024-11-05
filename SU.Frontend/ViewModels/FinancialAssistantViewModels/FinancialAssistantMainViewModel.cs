using System.Windows.Input;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.FinancialAssistantViewModels;

public class FinancialAssistantMainViewModel : ObservableObject
{
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

    // Commands
    public ICommand ToShowInsurances { get; set; }
    public ICommand ToShowCustomers { get; set; }
    public ICommand ToRegisterBillingInfo { get; set; }
    public ICommand ToRegisterProvision { get; set; }
    public ICommand ToShowSellingStat { get; set; }

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