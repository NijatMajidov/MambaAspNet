using MambaASPNET.Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace MambaASPNET.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public IActionResult Index()
        {
            var services = _teamService.GetAllTeams();
            return View(services);
        }

        public IActionResult Create()
        {
            return View();
        }
        
    }
}
