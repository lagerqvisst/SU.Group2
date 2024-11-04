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
        // ObservableCollections for commissions
        private ObservableCollection<Commission> _commissions;

        //Controller 
        private readonly ComissionController _commissionController;

        // Commands
        public ICommand GenerateCommissionCommand { get; }
        public ICommand ExportComissionExcel { get; }

        // Constructor
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

        #region Properties
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
        #endregion Properties

        // Method to export the commissions to an Excel file
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

        // Method to load all commissions from the controller
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
