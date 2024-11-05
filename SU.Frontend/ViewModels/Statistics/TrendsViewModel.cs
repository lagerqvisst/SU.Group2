using System.Windows.Input;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;

namespace SU.Frontend.ViewModels.Statistics;

public class TrendsViewModel : ObservableObject
{
    public INavigationService _navigationService;

    public TrendsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        ReturnCommnad = new RelayCommand(() => _navigationService.ReturnToPrevious());
    }

    public ICommand ReturnCommnad { get; set; }
}