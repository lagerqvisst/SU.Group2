using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SU.Frontend.Helper.Navigation;
using SU.Backend.Models.Employees;

namespace SU.Frontend.ViewModels.CeoMainViewModel
{    
    public class CeoMainViewModel : ObservableObject
    {
        // Commands to public properties
        public ICommand ToTrends { get; set; }
        public ICommand ToMonthlyStatistics { get; set; }
        public ICommand ToShowInsurances { get; set; }
        public ICommand ToShowCustomers { get; set; }
    

        public INavigationService _navigationService;
        public CeoMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToTrends = new RelayCommand(() => _navigationService.NavigateToTrends());
            ToMonthlyStatistics = new RelayCommand(() => _navigationService.NavigateToMonthlyStatistics());
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances());
            ToShowCustomers = new RelayCommand(() =>_navigationService.NavigateToShowCustomers());
        }

    }
}

