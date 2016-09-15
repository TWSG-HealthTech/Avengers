using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PowerPuff.Features.Medication.Models;
using Prism.Mvvm;

namespace PowerPuff.Features.Medication.Views
{
    public class MedicationMainViewModel : BindableBase
    {
        public ObservableCollection<DrugOrder> DrugOrders { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
               
        public MedicationMainViewModel()
        {
            DrugOrders = new ObservableCollection<DrugOrder>();
            IsLoading = true;
        }
    }
}