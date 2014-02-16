using System.Collections.Generic;
using System.Linq;
using Football.Repository;
using Football.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Football.Tests.Services
{
    [TestClass]
    public class TeamServiceTests : BaseTest
    {
        [TestMethod]
        public void GetTeamWithLeastGoalDifference_MultipleTeams_ReturnsFirstTeam()
        {
            var mockTeamRepository = MockRepository.CreateMockOf<ITeamRepository>(verifyList: VerifyList)
                .SetupGetTeamsFromLeague();

            ITeamService service = new TeamService(mockTeamRepository.Object);

            // Act
            var result = service.GetTeamWithLeastGoalDifference();

            // Assert
            result.IsInstanceOf<Team>();
            result.Name.IsEqualTo("Arsenal");
        }

        [TestMethod]
        public void GetTeamWithLeastGoalDifference_NoTeamsInRepository_ReturnsNull()
        {
            // Arrange
            var noTeams = new List<Team>();

            var mockTeamRepository = MockRepository.CreateMockOf<ITeamRepository>(verifyList: VerifyList)
                .SetupWithVerification(call => call.GetTeamsFromLeague(), result: noTeams);

            ITeamService service = new TeamService(mockTeamRepository.Object);

            // Act
            var result = service.GetTeamWithLeastGoalDifference();

            // Assert
            result.IsEqualTo(null);

        }

        [TestMethod]
        public void GetTeamWithLeastGoalDifference_TwoTeamsWithSameDifference_ReturnsFirstTeam()
        {
            var twoTeamsWithSameDifference = new List<Team>()
            {
                new Team("Arsenal", 10, 10),
                new Team("Manchester United", 10, 10)
            };

            var mockTeamRepository = MockRepository.CreateMockOf<ITeamRepository>(verifyList: VerifyList)
                .SetupWithVerification(call => call.GetTeamsFromLeague(), result: twoTeamsWithSameDifference);

            ITeamService service = new TeamService(mockTeamRepository.Object);

            // Act
            var result = service.GetTeamWithLeastGoalDifference();

            // Assert
            result.IsInstanceOf<Team>();
            result.Name.IsEqualTo("Arsenal");
        }

        [TestMethod]
        public void GetTeamWithWorstGoalDifference_MultipleTeams_ReturnsFirstTeam()
        {
         
            var mockTeamRepository = MockRepository.CreateMockOf<ITeamRepository>(verifyList: VerifyList)
                .SetupGetTeamsFromLeague();

            ITeamService service = new TeamService(mockTeamRepository.Object);

            // Act
            var result = service.GetTeamWithWorstGoalDifference();

            // Assert
            result.IsInstanceOf<Team>();
            result.Name.IsEqualTo("Manchester City");
        }

        [TestMethod]
        public void GetChampion_MultipleTeams_ReturnsTeam()
        {
            var mockTeamRepository = MockRepository.CreateMockOf<ITeamRepository>(verifyList: VerifyList)
                .SetupGetTeamsFromLeague();

            ITeamService service = new TeamService(mockTeamRepository.Object);

            // Act
            var result = service.GetChampion();

            // Assert
            result.IsInstanceOf<Team>();
            result.Name.IsEqualTo("Arsenal");
        }

        [TestMethod]
        public void GetRelegatedTeams_MultipleTeams_ReturnsBottomThree()
        {
            var mockTeamRepository = MockRepository.CreateMockOf<ITeamRepository>(verifyList: VerifyList)
                .SetupGetTeamsFromLeague();

            ITeamService service = new TeamService(mockTeamRepository.Object);

            // Act
            var result = service.GetRelegatedTeams();

            // Assert
            result.IsInstanceOf<IEnumerable<Team>>();
            result.ElementAt(2).Name.IsEqualTo("Newcastle United");
        }

        [TestMethod]
        public void AddPlayer_SavesToRepository_VerificationSuccess()
        {
            // Arrange
            Team someTeam = new Team("CoolTeam");
            var mockTeamRepository = MockRepository.CreateMockOf<ITeamRepository>(verifyList: VerifyList)
                .SetupWithVerification(call => call.Save(It.IsAny<Team>()));

            // Act 
            // Assert
            ITeamService service = new TeamService(mockTeamRepository.Object);
            service.AddPlayer(someTeam, "Cool Dude", "position", 0);
        }
    }
}
