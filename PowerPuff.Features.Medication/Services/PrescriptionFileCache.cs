using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPuff.Common.Helpers;
using PowerPuff.Features.Medication.Models;

namespace PowerPuff.Features.Medication.Services
{
    class PrescriptionFileCache : IPrescriptionCache
    {
        public PrescriptionFileCache()
        {
            Directory.CreateDirectory(GetPrescriptionFileDirectory());
        }

        public bool ExistPrescription(string patientId)
        {
            return File.Exists(GetPrescriptionFilePath(patientId));
        }

        public Task<Prescription> RetrievePrescription(string patientId)
        {
            return JsonFileUtils.ReadFromJsonFile<Prescription>(GetPrescriptionFilePath(patientId));
        }

        public Task StorePrescription(string patientId, Prescription prescription)
        {
            return JsonFileUtils.WriteToJsonFile(GetPrescriptionFilePath(patientId), prescription);
        }

        private string GetPrescriptionFilePath(string patientId)
        {
            return Path.Combine(GetPrescriptionFileDirectory(), $"{patientId}.json");
        }

        private string GetPrescriptionFileDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CATE",
                "Prescriptions");
        }
    }
}
