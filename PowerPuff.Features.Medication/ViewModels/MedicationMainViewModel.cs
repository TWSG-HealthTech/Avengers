using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using PowerPuff.Features.Medication.Models;
using Prism.Mvvm;
using Prism.Regions;

namespace PowerPuff.Features.Medication.ViewModels
{
    public class MedicationMainViewModel : BindableBase, INavigationAware
    {
        private readonly IPrescriptionService _prescriptionService;

        public ObservableCollection<DrugOrder> DrugOrders { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
               
        public MedicationMainViewModel(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
            DrugOrders = new ObservableCollection<DrugOrder>();
            IsLoading = false;
       } 

        public async Task LoadDrugOrders()
        {
            try
            {
                IsLoading = true;
                DrugOrders.AddRange(await _prescriptionService.GetDrugOrdersAsync());
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Failed to get Drug Orders due to {e.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await LoadDrugOrders();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}