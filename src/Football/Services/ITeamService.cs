using System.Collections.Generic;

namespace Football
{
    public interface ITeamService
    {
        Team GetTeamWithLeastGoalDifference();
        Team GetTeamWithWorstGoalDifference();
        Team GetChampion();
        IEnumerable<Team> GetRelegatedTeams();
        IEnumerable<Team> GetAllTeams();
        void AddPlayer(Team team, string name, string position, int goalsScored);
    }
}