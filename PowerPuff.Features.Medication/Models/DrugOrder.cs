using System;
using Prism.Mvvm;

namespace PowerPuff.Features.Medication.Models
{
    public class DrugOrder : BindableBase
    {
        private String _drugName;
        public String DrugName
        {
            get { return _drugName; }
            set { SetProperty(ref _drugName, value); }
        }

        private String _dose;
        public String Dose
        {
            get { return _dose; }
            set { SetProperty(ref _dose, value); }
        }

        private String _doseUnit;
        public String DoseUnit
        {
            get { return _doseUnit; }
            set { SetProperty(ref _doseUnit, value); }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        private String _frequency;
        public String Frequency
        {
            get { return _frequency; }
            set { SetProperty(ref _frequency, value); }
        }

    }
}
