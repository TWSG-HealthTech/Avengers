using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PowerPuff.Features.Medication.Models;

namespace PowerPuff.Features.Medication.ViewModels
{
    public interface IPrescriptionService
    {
        Task<Prescription> GetPrescriptionAsync(string patientId);
        Task<IEnumerable<DrugOrder>> GetDrugsToTake(string patientId, DateTime time, IEnumerable<string> frequencies);
    }
}
