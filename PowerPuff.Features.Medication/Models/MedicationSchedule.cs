using System;
using System.Collections.Generic;

namespace PowerPuff.Features.Medication.Models
{
    public class MedicationSchedule
    {
        public IEnumerable<string> frequencies;
        public string Name;
        public DateTime TimeInDay;
    }
}