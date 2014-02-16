using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Football.Repository
{
    public class TeamCsvRepository : ITeamRepository
    {
        // Our little memory database of teams and players
        private static List<Team> _teams;

        public TeamCsvRepository(string filePath)
        {
            if (_teams == null)
            {
                var stream = new LeagueDataCsvFileStream(filePath, FileMode.Open, FileAccess.Read);
                _teams = stream.GetTeamsFromFile().ToList();
            }
        }

        public IEnumerable<Team> GetTeamsFromLeague()
        {
            return _teams;
        }

        public void Save(Team team)
        {
            // Not much to do since this is memory only
        }
    }
}