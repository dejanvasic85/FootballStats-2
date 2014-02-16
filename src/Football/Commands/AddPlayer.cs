using System;
using System.Linq;
using Football.Services;

namespace Football.Commands
{
    [Help(Description = "Parameters: Team, PlayerName, Goals, Position (e.g. midfielder)")]
    public class AddPlayer : CommandBase
    {
        private readonly ITeamService _teamService;

        private string _playerName;
        private string _teamName;
        private string _position;
        private int _goalsScored;

        public AddPlayer(ILogService logService, ITeamService teamService) : base(logService)
        {
            _teamService = teamService;
        }

        protected override void ProcessArguments(CommandArguments args)
        {
            _playerName = args.ReadArgument("Name", isRequired: true);
            _teamName = args.ReadArgument("Team", isRequired: true);
            _position = args.ReadArgument("Position");
            _goalsScored = args.ReadArgument("Goals", isRequired: false, readDefault:  () => 0);
        }

        protected override void Go()
        {
            // Check whether teams exists
            var team = _teamService.GetAllTeams().FirstOrDefault( t => t.Name.Equals(_teamName, StringComparison.OrdinalIgnoreCase) );

            if (team == null)
            {
                LogService.Warning("The team does not exist, please try again.");
                return;
            }

            _teamService.AddPlayer(team, _playerName, _position, _goalsScored);
        }
    }
}