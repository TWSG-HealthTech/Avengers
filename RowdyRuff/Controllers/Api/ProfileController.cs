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
        
        public IActionResult Index(string profileId)
        {
            var profile = _clientProfileRepository.FindProfileBy(profileId);
            return Json(profile);
        }
    }
}
