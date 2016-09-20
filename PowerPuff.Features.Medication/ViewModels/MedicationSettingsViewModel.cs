using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPuff.Features.Medication.Models;

namespace PowerPuff.Features.Medication.ViewModels
{
    public class MedicationSettingsViewModel
    {
        public List<MedicationSchedule> MedicationSchedules { get; set; }

        public MedicationSettingsViewModel()
        {
            MedicationSchedules = new List<MedicationSchedule>();
            MedicationSchedules.Add(new MedicationSchedule() {Name = "Morning", TimeInDay = new DateTime(1,1,1,8,30,0), Frequencies = new []{"3"}});
            MedicationSchedules.Add(new MedicationSchedule() {Name = "Lunch", TimeInDay = new DateTime(1,1,1,12,0,0), Frequencies = new []{"3"}});
            MedicationSchedules.Add(new MedicationSchedule() {Name = "Dinner", TimeInDay = new DateTime(1,1,1,19,00,0), Frequencies = new []{"3"}});
        }
    }
}
