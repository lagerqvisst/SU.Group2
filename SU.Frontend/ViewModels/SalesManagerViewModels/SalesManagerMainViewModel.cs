﻿using SU.Frontend.Helper;
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
        // Commands
        public ICommand ToCreateExportStatistics { get; set; }
        public ICommand ToMonthlyStats { get; set; }
        public ICommand ToShowInsurances { get; set; }
        public ICommand ToShowCustomers { get; set; }
        public ICommand ToShowCustomerProspects { get; set; }

        // Service
        public INavigationService _navigationService;

        // Constructor
        public SalesManagerMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //Shared views are navigated through the navigation service
            ToCreateExportStatistics = new RelayCommand(() => _navigationService.NavigateToTrends());
            ToMonthlyStats = new RelayCommand(() => _navigationService.NavigateToMonthlyStatistics());
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances());
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers());
            ToShowCustomerProspects = new RelayCommand(() => _navigationService.NavigateToShowCustomerProspects());
        }
    }
}