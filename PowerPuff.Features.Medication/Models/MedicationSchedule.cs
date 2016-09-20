using System;
using System.Collections.Generic;

namespace PowerPuff.Features.Medication.Models
{
    public class MedicationSchedule
    {
        public IEnumerable<string> Frequencies { get; set; }
        public string Name { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}