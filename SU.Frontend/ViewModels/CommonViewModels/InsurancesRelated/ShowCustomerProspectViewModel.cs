using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Enums.Prospects;
using SU.Backend.Models.Insurances.Prospects;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated
{
    public class ShowCustomerProspectViewModel : ObservableObject
    {

        private ObservableCollection<Prospect> _prospects;
        private readonly ProspectController _prospectController;

        public ShowCustomerProspectViewModel(ProspectController prospectController)
        {
            _prospectController = prospectController;
            LoadProspectsAsync();
        }

        public ObservableCollection<Prospect> Prospects
        {
            get => _prospects;
            set
            {
                _prospects = value;
                OnPropertyChanged();
            }
        }

        private DateTime _contactDate;
        public DateTime ContactDate
        {
            get => _contactDate;
            set
            {
                _contactDate = value;
                OnPropertyChanged();
            }
        }

        private async Task LoadProspectsAsync()
        {
            var result = await _prospectController.GetAllCurrentProspects();
            if (result.prospects != null)
            {
                Prospects.Clear();
                
                foreach (var prospect in result.prospects)
                {
                    Prospects.Add(prospect);
                }
            }
          
        }


    }





}




