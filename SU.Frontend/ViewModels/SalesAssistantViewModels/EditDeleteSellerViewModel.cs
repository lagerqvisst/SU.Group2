using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Insurances;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.SalesAssistantViewModels
{
    public class EditDeleteSellerViewModel : ObservableObject
    {
        private readonly EmployeeController _employeeController;

        public ICommand SaveEmployeeCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }

        public ObservableCollection<Employee> Sellers { get; set; } = new ObservableCollection<Employee>();

        private Employee _selectedSeller;
        public Employee SelectedSeller
        {
            get => _selectedSeller;
            set
            {
                _selectedSeller = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedSellerAsCollection)); // Update DataGrid
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

    }
}
