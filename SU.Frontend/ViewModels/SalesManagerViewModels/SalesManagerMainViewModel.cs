using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.SalesManagerViewModels
{
    public class SalesManagerViewModel : ObservableObject
    {
        public ICommand ToCreateExportStatistics;
        public ICommand ToShowInsurances;
        public ICommand ToShowCustomers;
        public INavigationService _navigationService;
        public SalesManagerViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToCreateExportStatistics = new RelayCommand(() => _navigationService.NavigateToCreateExportStatistics(_navigationService));
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances(_navigationService));
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers(_navigationService));
        }
    }
}
