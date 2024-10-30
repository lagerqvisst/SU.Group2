using SU.Backend.Controllers;
using SU.Backend.Models.Statistics;
using SU.Frontend.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SU.Frontend.ViewModels.CommonViewModels.InsurancesRelated
{


        public class CreateSellStatViewModel : ObservableObject
        {
            private readonly StatisticsController _statisticsController;

            // Lista över säljare
            public ObservableCollection<SellerStatistics> Sellers { get; set; }

            // Vald säljare
            private SellerStatistics _selectedSeller;
            public SellerStatistics SelectedSeller
            {
                get => _selectedSeller;
                set
                {
                    _selectedSeller = value;
                    OnPropertyChanged();
                    LoadSellerStatistics();  // Ladda statistik när säljare ändras
                }
            }

            // Statistik för vald säljare
            public ObservableCollection<MonthlySalesData> SellerStatistics { get; set; }

            public CreateSellStatViewModel(StatisticsController statisticsController)
            {
                _statisticsController = statisticsController;

                Sellers = new ObservableCollection<SellerStatistics>();
                SellerStatistics = new ObservableCollection<MonthlySalesData>();

                // Ladda säljare
                LoadSellers();
            }

            private async void LoadSellers()
            {
                // Hämta statistik för alla säljare för ett specifikt år (t.ex. 2023)
                var result = await _statisticsController.GetSellerStatistics(2023);

                if (result.Item2 != null)
                {
                    foreach (var seller in result.Item2)
                    {
                        Sellers.Add(seller);
                    }
                }
            }

            private void LoadSellerStatistics()
            {
                if (SelectedSeller != null)
                {
                    SellerStatistics.Clear();
                    foreach (var monthlyData in SelectedSeller.MonthlySales)
                    {
                        SellerStatistics.Add(monthlyData);
                    }
                }
            }
        }
    }









