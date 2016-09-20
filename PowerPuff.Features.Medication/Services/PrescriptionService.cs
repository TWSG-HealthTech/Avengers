using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PowerPuff.Common.Gateway;
using PowerPuff.Common.Helpers;
using PowerPuff.Common.Settings;
using PowerPuff.Features.Medication.Models;
using PowerPuff.Features.Medication.ViewModels;

namespace PowerPuff.Features.Medication.Services
{
    public class PrescriptionService : IPrescriptionService
    {        
        private readonly IServerGateway _serverGateway;
        private readonly IPrescriptionCache _prescriptionCache;

        public PrescriptionService(IServerGateway serverGateway, IPrescriptionCache prescriptionCache)
        {
            _serverGateway = serverGateway;
            _prescriptionCache = prescriptionCache;
        }

        public async Task<Prescription> GetPrescriptionAsync(string patientId)
        {
            if (_prescriptionCache.ExistPrescription(patientId))
            {
                return await _prescriptionCache.RetrievePrescription(patientId);
            }

            var drugOrders = await _serverGateway.GetAsync<List<DrugOrder>>($"medication/api/{patientId}");
            var prescription = new Prescription()
            {
                DrugOrders = drugOrders
            };
            await _prescriptionCache.StorePrescription(patientId, prescription);
            return prescription;
        }

        public async Task<IEnumerable<DrugOrder>> GetDrugsToTake(string patientId, DateTime time, IEnumerable<string> frequencies)
        {
            var prescription = await GetPrescriptionAsync(patientId);
            return prescription.GetDrugOrdersForDateTimeWithFrequencies(time, frequencies);
        }
    }
}
