using System.Web.Mvc;

namespace Football.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamService _teamService;

        public HomeController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        
        // GET
        public ActionResult Index()
        {
            var teams = _teamService.GetAllTeams();

            // Better practice is to use automapper and map this domain model to a view model

            return View(teams);
        }
    }
}