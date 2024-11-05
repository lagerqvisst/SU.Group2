using SU.Backend.Controllers;
using SU.Backend.Models.Comissions;
using SU.Backend.Models.Commissions;
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

        private ObservableCollection<Month> _months;
        public ObservableCollection<Month> Months
        {
            get => _months;
            set
            {
                _months = value;
                OnPropertyChanged();
            }
        }

        private Month _selectedMonth;
        public Month SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                OnPropertyChanged();
                UpdateDateRange();
            }
        }


        //Controller 
        private readonly ComissionController _commissionController;

        // Commands
        public ICommand GenerateCommissionCommand { get; }
        public ICommand ExportComissionExcel { get; }

        // Constructor
        public CommissionViewModel(ComissionController commissionController)
        {
            _commissionController = commissionController;

            Months = new ObservableCollection<Month>
            {
                new Month { MonthName = "January", MonthNumber = 1 },
                new Month { MonthName = "February", MonthNumber = 2 },
                new Month { MonthName = "March", MonthNumber = 3 },
                new Month { MonthName = "April", MonthNumber = 4 },
                new Month { MonthName = "May", MonthNumber = 5 },
                new Month { MonthName = "June", MonthNumber = 6 },
                new Month { MonthName = "July", MonthNumber = 7 },
                new Month { MonthName = "August", MonthNumber = 8 },
                new Month { MonthName = "September", MonthNumber = 9 },
                new Month { MonthName = "October", MonthNumber = 10 },
                new Month { MonthName = "November", MonthNumber = 11 },
                new Month { MonthName = "December", MonthNumber = 12 }
            };

            SelectedMonth = Months.FirstOrDefault(m => m.MonthNumber == DateTime.Now.Month);

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

        private void UpdateDateRange()
        {
            if (SelectedMonth != null)
            {
                int year = DateTime.Now.Year;
                StartDate = new DateTime(year, SelectedMonth.MonthNumber, 1);
                EndDate = StartDate.AddMonths(1).AddDays(-1);
            }
        }
    }
}
