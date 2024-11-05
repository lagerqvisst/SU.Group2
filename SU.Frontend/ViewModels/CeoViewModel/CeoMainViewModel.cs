using System.Windows.Input;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.CeoMainViewModel;

public class CeoMainViewModel : ObservableObject
{
    // Service
    public INavigationService _navigationService;

    // Constructor
    public CeoMainViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        //Shared views are navigated through the navigation service
        ToTrends = new RelayCommand(() => _navigationService.NavigateToTrends());
        ToMonthlyStatistics = new RelayCommand(() => _navigationService.NavigateToMonthlyStatistics());
        ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances());
        ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers());
    }

    // Commands
    public ICommand ToTrends { get; set; }
    public ICommand ToMonthlyStatistics { get; set; }
    public ICommand ToShowInsurances { get; set; }
    public ICommand ToShowCustomers { get; set; }
}