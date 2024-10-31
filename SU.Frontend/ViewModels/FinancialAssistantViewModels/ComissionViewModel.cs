using SU.Backend.Controllers;
using SU.Backend.Models.Comissions;
using SU.Frontend.Helper;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.FinancialAssistantViewModels
{
    public class CommissionViewModel : ObservableObject
    {
        private ObservableCollection<Commission> _commissions;
        private readonly ComissionController _commissionController;

        public CommissionViewModel(ComissionController commissionController)
        {
            _commissionController = commissionController;
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            EndDate = DateTime.Now;
            Commissions = new ObservableCollection<Commission>();
            GenerateCommissionCommand = new RelayCommand(async () => await LoadCommissionsAsync());
            ExportComissionExcel = new RelayCommand(ExportComissionToExcel);

            LoadCommissionsAsync().ConfigureAwait(false);

        }

        public ObservableCollection<Commission> Commissions
        {
            get => _commissions;
            set
            {
                _commissions = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public ICommand GenerateCommissionCommand { get; }
        public ICommand ExportComissionExcel { get; }

        private async void ExportComissionToExcel()
        {
            var commissionList = Commissions.ToList();

            var (success, message) = await _commissionController.ExportCommissionsToExcel(commissionList);
            if (success)
            {
                MessageBox.Show(
                    message,
                    "Export Successful",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
            else
            {
                MessageBox.Show(
                    message,
                    "Export Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }



        private async Task LoadCommissionsAsync()
        {
            var (message, commissions) = await _commissionController.GetCommissions(StartDate, EndDate);
            if (commissions != null)
            {
                Commissions.Clear();
                foreach (var commission in commissions)
                {
                    Commissions.Add(commission);
                }
            }
        }
    }
}
