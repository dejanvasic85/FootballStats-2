using System.Collections.Generic;

namespace Football.Repository
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetTeamsFromLeague();
        void Save(Team team);
    }
}