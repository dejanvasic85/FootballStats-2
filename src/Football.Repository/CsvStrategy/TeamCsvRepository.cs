using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Football.Repository
{
    public class TeamCsvRepository : ITeamRepository
    {
        private readonly string _filePath;
        
        // Our little memory database of teams and players ( nicely used for the console application )
        // Note: this is not thread safe at the moment, a lock object can be used 
        // e.g. private object _sync = new object();
        // then use the lock(_sync) when operating on the list
        // Or use one of the ConcurrentBag collection
        private static List<Team> _teams;


        public TeamCsvRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Team> GetTeamsFromLeague()
        {
            // Caching
            if (_teams != null)
                return _teams;

            using (var stream = new LeagueDataCsvFileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                _teams = stream.GetTeamsFromFile().ToList();
            }


            return _teams;
        }

        public void Save(Team team)
        {
            // Not much to do since this is memory only
        }
    }
}