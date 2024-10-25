using SU.Backend.Models.Enums.Insurance;
using SU.Frontend.Helper;
using SU.Frontend.Helper.DI_Objects.InsuranceObjects;
using SU.Frontend.Helper.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.CommonViewModels.NewInsurance
{
    public class PrivateInsuranceTypeViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IInsuranceTypeService _insuranceTypeService;

        // Collection of private insurance types
        private List<InsuranceType> _privateInsuranceTypes;
        public List<InsuranceType> PrivateInsuranceTypes
        {
            get => _privateInsuranceTypes;
            set
            {
                _privateInsuranceTypes = value;
                OnPropertyChanged();
            }
        }

        // Selected insurance type
        private InsuranceType _selectedInsuranceType;
        public InsuranceType SelectedInsuranceType
        {
            get => _selectedInsuranceType;
            set
            {
                _selectedInsuranceType = value;
                OnPropertyChanged();
            }
        }

        // Command for continue button
        public ICommand ContinueCommand { get; }

        public PrivateInsuranceTypeViewModel(INavigationService navigationService, IInsuranceTypeService insuranceTypeService)
        {
            _navigationService = navigationService;
            _insuranceTypeService = insuranceTypeService;

            // Initialize the list of private insurance types (filtered)
            PrivateInsuranceTypes = Enum.GetValues(typeof(InsuranceType))
                .Cast<InsuranceType>()
                .Where(t => IsPrivateInsuranceType(t))
                .ToList();

            // Command for the continue button
            ContinueCommand = new RelayCommand(OnContinue, CanContinue);
        }

        // Filter the enum to only include private types
        private bool IsPrivateInsuranceType(InsuranceType type)
        {
            return type == InsuranceType.ChildAccidentAndHealthInsurance
                || type == InsuranceType.AdultAccidentAndHealthInsurance
                || type == InsuranceType.AdultLifeInsurance;
        }

        // Continue logic
        private void OnContinue()
        {
            _insuranceTypeService.InsuranceType = SelectedInsuranceType;
            _navigationService.NavigateTo("", "");
        }

        // Validation for the continue button
        private bool CanContinue()
        {
            return SelectedInsuranceType != null;
        }
    }
}
