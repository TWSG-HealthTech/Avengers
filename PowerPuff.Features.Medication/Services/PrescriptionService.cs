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
    public class PrescriptionService : ServerGatewayBase, IPrescriptionService
    {
        private const string Url = "medication/api/123";
        private readonly string _localDrugOrdersPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DrugOrders.json");

        public PrescriptionService(IApplicationSettings applicationSettings) : base(applicationSettings)
        {
        }

        public async Task<List<DrugOrder>> GetDrugOrdersAsync()
        {
            if (File.Exists(_localDrugOrdersPath))
            {
                return await JsonFileUtils.ReadFromJsonFile<List<DrugOrder>>(_localDrugOrdersPath);
            }

            var drugOrders = await GetAsync<List<DrugOrder>>(Url);
            await JsonFileUtils.WriteToJsonFile(_localDrugOrdersPath, drugOrders);

            return drugOrders;
        }
    }
}
