using Football.Repository;
using Football.Services;

namespace Football.Commands
{
    [Help(Description = "Displays all the available teams.")]
    public class ShowAllTeams : CommandBase
    {
        private readonly ITeamRepository _teamRepository;
        private bool _showGoals;
        private bool _showPlayers;

        public ShowAllTeams(ILogService logService, ITeamRepository teamRepository)
            : base(logService)
        {
            _teamRepository = teamRepository;
        }

        protected override void ProcessArguments(CommandArguments args)
        {
            // Example of how arguments can be handled
            _showGoals = args.ReadArgument("ShowGoals", isRequired: false, readDefault: () => false);
            _showPlayers = args.ReadArgument("ShowPlayers", isRequired: false, readDefault: () => true);
        }

        protected override void Go()
        {
            // Straight to the repo... maybe a little nicer to use the service
            _teamRepository
                .GetTeamsFromLeague()
                .ForEach(team =>
                {
                    LogService.Log(_showGoals
                        ? string.Format("[{0}]\t{1, 3}\t{2}\t{3, 3}", team.Position, team.Name, team.GoalsFor, team.GoalsAgainst)
                        : string.Format("[{0}]\t{1, 3}", team.Position, team.Name));

                    if (_showPlayers)
                    {
                        team.Players.ForEach(player => LogService.Log(string.Format("{0, 5} {1}", "Name: ", player.Name)));
                    }
                });

        }
    }
}