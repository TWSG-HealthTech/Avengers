using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPuff.Features.Medication.Models;

namespace PowerPuff.Features.Medication.Services
{
    public interface IPrescriptionCache
    {
        bool ExistPrescription(string patientId);
        Task<Prescription> RetrievePrescription(string patientId);
        Task StorePrescription(string patientId, Prescription prescription);
    }
}
