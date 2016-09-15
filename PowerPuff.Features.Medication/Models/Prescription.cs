using System.Collections.Generic;
using Prism.Mvvm;

namespace PowerPuff.Features.Medication.Models
{
    public class Prescription : BindableBase
    {
        private List<DrugOrder> _drugOrders;

        public List<DrugOrder> DrugOrders
        {
            get { return _drugOrders; }
            set { SetProperty(ref _drugOrders, value); }
        }
    }
}
