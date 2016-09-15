using System.Collections.Generic;
using System.Threading.Tasks;
using PowerPuff.Features.Medication.Models;

namespace PowerPuff.Features.Medication.ViewModels
{
    public interface IPrescriptionService
    {
        Task<List<DrugOrder>> GetDrugOrdersAsync();
    }
}
