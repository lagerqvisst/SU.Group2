using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.Helper.Return
{
    public class ReturnService : IReturnService
    {
        private readonly INavigationService _navigationService;
        public ReturnService(INavigationService navigationService)
        {
        

            _navigationService = navigationService;

        }

        public void ReturnToMain()
        {
            _navigationService.CloseAllExcept("LoginWindow");
        }
    }




}
