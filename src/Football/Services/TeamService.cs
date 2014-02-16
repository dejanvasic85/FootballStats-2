using System;
using Football.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Football
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;

        public TeamService(ITeamRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Returns the team with the least amount of difference between goals scored and goals conceded
        /// </summary>
        public Team GetTeamWithLeastGoalDifference()
        {
            return _repository.GetTeamsFromLeague()
                .OrderBy(team => team.GetGoalDifference())
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns the team which has conceded more goals than scored
        /// </summary>
        /// <returns></returns>
        public Team GetTeamWithWorstGoalDifference()
        {
            return _repository.GetTeamsFromLeague()
                .OrderByDescending(team => team.GetGoalDifference(useAbsoluteValue: false))
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns the team that holds the first position
        /// </summary>
        public Team GetChampion()
        {
            return _repository.GetTeamsFromLeague().Single(team => team.Position == 1);
        }

        /// <summary>
        /// Returns the team that are in the bottom three of the list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Team> GetRelegatedTeams()
        {
            // Returns 
            return _repository.GetTeamsFromLeague()
                .OrderByDescending(team => team.Position)
                .Take(3)
                .OrderBy(team => team.Position);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _repository.GetTeamsFromLeague();
        }

        /// <summary>
        /// Adds a player to the team and commits to the repository
        /// </summary>
        public void AddPlayer(Team team, string name, string position, int goalsScored)
        {
            team.Players.Add(new Player
            {
                Name = name,
                Team = team,
                Position = position,
                GoalsScored = goalsScored
            });

            _repository.Save(team);
        }
    }
}