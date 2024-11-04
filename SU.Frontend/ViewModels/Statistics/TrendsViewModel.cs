using Microsoft.EntityFrameworkCore.Metadata;
using SU.Frontend.Helper;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.Statistics
{
    public class TrendsViewModel : ObservableObject
    {
        public ICommand ReturnCommnad { get; set; }
        public INavigationService _navigationService;

        public TrendsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ReturnCommnad = new RelayCommand(() => _navigationService.ReturnToPrevious());
        }

    }
}
