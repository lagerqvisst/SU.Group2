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
    public class SalesManagerMainViewModel : ObservableObject
    {
        public ICommand ToCreateExportStatistics { get; set; }
        public ICommand ToShowInsurances { get; set; }
        public ICommand ToShowCustomers { get; set; }

        public INavigationService _navigationService;
        public SalesManagerMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //Shared views are navigated through the navigation service
            ToCreateExportStatistics = new RelayCommand(() => _navigationService.NavigateToCreateExportStatistics());
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances());
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers());
        }
    }
}
