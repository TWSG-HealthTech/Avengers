using System;
using System.Collections.Generic;

namespace PowerPuff.Features.Medication.Models
{
    public class MedicationSchedule
    {
        public IEnumerable<string> Frequencies;
        public string Name;
        public DateTime TimeInDay;
    }
}