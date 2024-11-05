using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using SU.Backend.Controllers;
using SU.Backend.Helper;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Frontend.Helper;

namespace SU.Frontend.ViewModels.SalesAssistantViewModels;

public class EditDeleteSellerViewModel : ObservableObject
{
    // Controller
    private readonly EmployeeController _employeeController;

    // Constructor
    public EditDeleteSellerViewModel(EmployeeController employeeController)
    {
        _employeeController = employeeController;

        LoadSellersAsync().ConfigureAwait(false);

        SaveSellerCommand = new RelayCommand(SaveSeller);
        DeleteSellerCommand = new RelayCommand(DeleteSeller);
    }

    // Commands
    public ICommand SaveSellerCommand { get; }
    public ICommand DeleteSellerCommand { get; }

    // ObservableCollections for sellers
    public ObservableCollection<Employee> Sellers { get; set; } = new();

    // List of seller types (Outside and Inside sales)
    public List<EmployeeType> SellerRoles { get; } = new()
    {
        EmployeeType.InsideSales,
        EmployeeType.OutsideSales
    };

    // Load all sellers
    private async Task LoadSellersAsync()
    {
        var result = await _employeeController.GetAllSellers();
        if (result.success && result.salesEmployees != null)
        {
            Sellers.Clear();
            foreach (var seller in result.salesEmployees) Sellers.Add(seller);
        }
        else
        {
            Console.WriteLine("Failed to load sellers: " + result.message);
        }
    }

    // Method to delete a seller
    private async void DeleteSeller()
    {
        var confirm = MessageBox.Show("Are you sure you want to delete this seller?",
            "Confirm Delete",
            MessageBoxButton.YesNo);

        if (confirm != MessageBoxResult.Yes) return;

        // Continue with deletion if user confirms
        if (SelectedSeller != null)
        {
            await _employeeController.DeleteEmployee(SelectedSeller);
            Sellers.Remove(SelectedSeller);
            SelectedSeller = null;
        }
    }

    // Method to save a seller
    private async void SaveSeller()
    {
        if (SelectedSeller != null)
        {
            var result = await _employeeController.UpdateEmployee(SelectedSeller);

            if (result.success)
                MessageBox.Show($"{result.message}, MessageBoxButton.OK");
            else
                MessageBox.Show($"{result.message}, MessageBoxButton.OK");
        }
    }

    // Method to update the selected seller type
    private void OnSelectedSellerChanged()
    {
        if (SelectedSeller != null)
            SelectedSellerType = EmployeeHelper.GetLowestPercentageRole(SelectedSeller.RoleAssignments.ToList());
    }

    #region Properties

    private EmployeeType _selectedSellerType;

    public EmployeeType SelectedSellerType
    {
        get => _selectedSellerType;
        set
        {
            _selectedSellerType = value;
            OnPropertyChanged();
            EmployeeHelper.UpdateEmployeeRole(SelectedSeller, _selectedSellerType);
        }
    }

    private Employee _selectedSeller;

    public Employee SelectedSeller
    {
        get => _selectedSeller;
        set
        {
            _selectedSeller = value;
            OnPropertyChanged();
            OnSelectedSellerChanged(); // Update DataGrid
        }
    }

    public IEnumerable<Employee> SelectedSellerAsCollection
    {
        get
        {
            if (SelectedSeller != null)
                return new List<Employee> { SelectedSeller };
            return null;
        }
    }

    #endregion Properties
}