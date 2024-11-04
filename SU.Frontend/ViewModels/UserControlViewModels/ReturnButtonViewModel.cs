using SU.Frontend.Helper.DI_Objects.User;
using SU.Frontend.Helper.Navigation;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.UserControlViewModels
{
    public class ReturnButtonViewModel
    {
        public ICommand ReturnCommand { get; }
        public INavigationService _navigationService { get; }

        public ReturnButtonViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ReturnCommand = new RelayCommand(ReturnToPrevious);
        }

        public void ReturnToPrevious()
        {
            _navigationService.ReturnToPrevious();
        }
    }
}
