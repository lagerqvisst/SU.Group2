﻿using SU.Frontend.Helper.Authentication;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.UserControlViewModels
{
    public class LogoutButtonViewModel
    {
        public ICommand LogoutCommand { get; }

        public LogoutButtonViewModel(IAuthenticationService authenticationService)
        {
            LogoutCommand = new RelayCommand(authenticationService.Logout);
        }
    }
}