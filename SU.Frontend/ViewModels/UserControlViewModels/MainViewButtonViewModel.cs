using SU.Frontend.Helper.Authentication;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.UserControlViewModels
{
    public class MainViewButtonViewModel
    {

        public ICommand MainViewCommand { get; }

        public MainViewButtonViewModel(IAuthenticationService authenticationService)
        {
            LogoutCommand = new RelayCommand(authenticationService.);
        }
    }
}
