using Microsoft.AspNetCore.Mvc;
using RowdyRuff.Core.Common;

namespace RowdyRuff.Controllers.Api
{
    public class ProfileController : Controller
    {
        private readonly IClientProfileRepository _clientProfileRepository;

        public ProfileController(IClientProfileRepository clientProfileRepository)
        {
            _clientProfileRepository = clientProfileRepository;
        }
        
        public IActionResult Connections(string profileId)
        {
            var connections = _clientProfileRepository.FindAllSocialConnectionsBy(profileId);
            return Json(connections);
        }
    }
}
