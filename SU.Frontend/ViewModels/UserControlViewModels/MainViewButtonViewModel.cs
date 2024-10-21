using SU.Frontend.Helper.Authentication;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SU.Frontend.Helper.Navigation;
using SU.Backend.Models.Employees;

namespace SU.Frontend.ViewModels.UserControlViewModels
{
    public class MainViewButtonViewModel
    {

        public ICommand ReturnToMainViewCommand { get; }

        public MainViewButtonViewModel(Employee employee, INavigationService navigationService)
        {
            ReturnToMainViewCommand = new RelayCommand(() => navigationService.ReturnToMain(employee, navigationService));
        }
    }
}
