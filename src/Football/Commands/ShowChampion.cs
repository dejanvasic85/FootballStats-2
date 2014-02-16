using Football.Services;

namespace Football.Commands
{
    [HelpAttribute(Description =  "Reveals the name of the team on top of the league. ")]
    public class ShowChampion : CommandBase
    {
        private readonly ITeamService _teamService;

        public ShowChampion(ILogService logService, ITeamService teamService) : base(logService)
        {
            _teamService = teamService;
        }

        protected override void ProcessArguments(CommandArguments args)
        {
            // No arguments required
        }

        protected override void Go()
        {
            Team championTeam = _teamService.GetChampion();

            LogService.Log(string.Format("And the champion is: {0}!!!!!", championTeam.Name));
        }
    }
}
