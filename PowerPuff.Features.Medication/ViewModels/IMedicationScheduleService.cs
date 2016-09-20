using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPuff.Features.Medication.Models;

namespace PowerPuff.Features.Medication.ViewModels
{
    public interface IMedicationScheduleService
    {
        event Action<MedicationSchedule> OnMedicationSchedule;
        void AddSchedule(MedicationSchedule schedule);
        void RemoveSchedule(MedicationSchedule schedule);
        IEnumerable<MedicationSchedule> GetAllSchedules();
    }
}