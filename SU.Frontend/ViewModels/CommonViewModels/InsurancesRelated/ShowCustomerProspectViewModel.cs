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
using System.Windows;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated
{
    public class ShowCustomerProspectViewModel : ObservableObject
    {

        private ObservableCollection<Prospect> _prospects;
        private readonly ProspectController _prospectController;


        public ICommand SaveProspectCommand { get; }

        public ShowCustomerProspectViewModel(ProspectController prospectController)
        {
            _prospectController = prospectController;
            Prospects = new ObservableCollection<Prospect>();
            LoadProspectsAsync();
            SaveProspectCommand = new RelayCommand(SaveProspect);
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
            await LoadAllProspectsAsync();
        }

        private async Task LoadAllProspectsAsync()
        {
            var prospectIdentify = await _prospectController.IdentifyNewProspects();
            var prospectResult = await _prospectController.GetAllCurrentProspects();
            if (prospectResult.prospects?.Any() ?? false)
            {
                Prospects.Clear();
                foreach (var prospects in prospectResult.prospects)
                {
                    Prospects.Add(prospects);
                }
            }
            else
            {
                Console.WriteLine("No prospects found");
            }
        }

        private Prospect _selectedProspect;
        public Prospect SelectedProspect
        {
            get => _selectedProspect;
            set
            {
                _selectedProspect = value;
                OnPropertyChanged();
               // OnSelectedProspectChanged();
            }
        }




        private async void SaveProspect()
        {
            var confirm = MessageBox.Show("Prospect has been updated",
                      "Confirm", MessageBoxButton.OK);

            if (SelectedProspect != null)
            {
                await _prospectController.UpdateProspect(SelectedProspect);
            }
            else if (SelectedProspect != null)
            {
                await _prospectController.UpdateProspect(SelectedProspect);
            }
        }

    }





}




