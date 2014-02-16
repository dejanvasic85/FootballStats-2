using System.Collections.Generic;
using Football.Repository;
using Moq;

namespace Football.Tests
{
    /// <summary>
    /// Common test data for the Team Repository class
    /// </summary>
    internal static class TeamRepositoryMocks
    {
        public static Mock<ITeamRepository> SetupGetTeamsFromLeague(this Mock<ITeamRepository> mock)
        {
            var teams = new List<Team>
            {
                new Team("Arsenal", 10, 9) { Position = 1},             // Expected return
                new Team("Manchester United", 10, 8) { Position = 2},   
                new Team("Manchester City", 10, -20) { Position = 3},
                new Team("Liverpool", 15, 17) { Position = 4},
                new Team("Newcastle United", 15, 17) { Position = 5},
            };

            mock.Setup(call => call.GetTeamsFromLeague()).Returns(teams);
            return mock;
        }
    }
}