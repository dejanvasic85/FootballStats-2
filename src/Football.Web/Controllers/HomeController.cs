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
        
        // GET all teams
        public ActionResult Index()
        {
            var teams = _teamService.GetAllTeams();

            // Better practice is to use automapper and map this domain model to a view model

            return View(teams);
        }

        // GET Champion
        public ActionResult ListChampion()
        {
            var team = _teamService.GetChampion();

            return Json(team, JsonRequestBehavior.AllowGet);
        }

        // GET teams for relegation
        public ActionResult ListRelegationZone()
        {
            var teams = _teamService.GetRelegatedTeams();

            return Json(teams, JsonRequestBehavior.AllowGet);
        }

        // GET team with least goal difference
        public ActionResult ListTeamWithLeastDifference()
        {
            var team = _teamService.GetTeamWithLeastGoalDifference();

            return Json(team, JsonRequestBehavior.AllowGet);
        }
    }
}