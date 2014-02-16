using Football.Services;

namespace Football.Commands
{
    [Help(Description = "Displays the team with least difference of goals scored and conceded")]
    public class ShowTeamWithLeastGoalDifference : CommandBase
    {
        private readonly ITeamService _teamService;

        public ShowTeamWithLeastGoalDifference(ILogService logService, ITeamService teamService) : base(logService)
        {
            _teamService = teamService;
        }

        protected override void ProcessArguments(CommandArguments args)
        {
            // No args required
        }

        protected override void Go()
        {
            var team = _teamService.GetTeamWithLeastGoalDifference();
            LogService.Log( string.Format("[{0}] {1} - Goal Difference: {2}", team.Position, team.Name, team.GetGoalDifference()));
        }
    }
}