using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<DrugOrder> GetDrugOrdersForDateTimeWithFrequencies(DateTime time,
            IEnumerable<string> frequencies)
        {
            return DrugOrders
                .Where(d => d.StartDate.CompareTo(time) <= 0 && d.EndDate.CompareTo(time) >= 0)
                .Where(d => frequencies.Contains(d.Frequency))
                .ToList();
        }
    }
}
