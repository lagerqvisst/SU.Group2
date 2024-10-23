﻿using SU.Frontend.Helper;
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
        public ICommand ToCreateExportStatistics;
        public ICommand ToShowInsurances;
        public ICommand ToShowCustomers;
        public INavigationService _navigationService;
        public CeoMainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ToCreateExportStatistics = new RelayCommand(NavigateToCeosExportStatistics);
            ToShowInsurances = new RelayCommand(() => _navigationService.NavigateToShowInsurances(_navigationService));
            ToShowCustomers = new RelayCommand(() => _navigationService.NavigateToShowCustomers(_navigationService));
        }

        public void NavigateToCeosExportStatistics()
        {
            _navigationService.NavigateTo("CreateExportStatisticsView");
        }

    }
}

