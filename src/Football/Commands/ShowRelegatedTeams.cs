using Football.Services;

namespace Football.Commands
{
    [Help(Description = "Reveals the teams that will be relegated")]
    public class ShowRelegatedTeams : CommandBase
    {
        private readonly ITeamService _teamService;

        public ShowRelegatedTeams(ILogService logService, ITeamService teamService)
            : base(logService)
        {
            _teamService = teamService;
        }

        protected override void ProcessArguments(CommandArguments args)
        {
            // No arguments required
        }

        protected override void Go()
        {
            _teamService.GetRelegatedTeams()
                .ForEach(team => base.LogService.Log(string.Format("[{0}] {1}", team.Position, team.Name)));
        }
    }
}