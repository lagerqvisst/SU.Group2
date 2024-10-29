using SU.Backend.Controllers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums.Insurance;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Frontend.ViewModels.CommonViewModels.CustomerRelated
{
    public class ShowCustomerViewModel : ObservableObject
    {
        private readonly EmployeeController _employeeController;

        // Använd ObservableCollection istället för List
        public ObservableCollection<Employee> Employees { get; set; }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(); // Notifierar UI om förändringen
                _ = OnSelectedEmployeeChangedAsync();
            }
        }

        // Egenskap för att hålla de valda detaljerna
        private Employee _selectedEmployeeDetails;
        public Employee SelectedEmployeeDetails
        {
            get => _selectedEmployeeDetails;
            set
            {
                _selectedEmployeeDetails = value;
                OnPropertyChanged(); // Uppdaterar UI med de nya detaljerna
            }
        }

        // Konstruktorn
        public ShowCustomerViewModel(EmployeeController employeeController)
        {
            _employeeController = employeeController;

            // Hämta anställda från EmployeeController och initiera Employees
            Employees = new ObservableCollection<Employee>((IEnumerable<Employee>)_employeeController.GetAllEmployees());
        }

        // Den asynkrona metoden som hanterar valet av en anställd
        private async Task OnSelectedEmployeeChangedAsync()
        {
            if (SelectedEmployee != null)
            {
                // Ladda detaljer om den valda anställda (simulerat här)
                SelectedEmployeeDetails = await LoadEmployeeDetailsAsync(SelectedEmployee);
            }
        }

        // Simulerad metod för att ladda detaljer om en anställd
        private async Task<Employee> LoadEmployeeDetailsAsync(Employee employee)
        {
            // Simulera en asynkron operation (exempelvis ett API-anrop eller databasfråga)
            await Task.Delay(500); // Simulerar en laddningstid, ersätt detta med riktig kod

            // Returnera detaljer om den valda anställda (i verkligheten skulle du ladda dessa från en datakälla)
            return _employeeController.GetAllEmployeeDetails(employee);
        }
    }










}
