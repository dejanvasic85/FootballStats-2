using Football.Commands;
using Football.Repository;
using Football.Services;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Football.Tests.Commands
{
    [TestClass]
    public class ShowRelegatedTeamsTests : BaseTest
    {
        [TestMethod]
        public void Go_ShowRelegatedTeams_VerificationSuccessful()
        {
            // Arrange
            MockRepository.CreateMockOf<ILogService>(Container, VerifyList)
                .SetupWithVerification(call => call.Log(It.IsAny<string>()))
                .SetupWithVerification(call => call.Info(It.IsAny<string>()));

            MockRepository.CreateMockOf<ITeamService>(Container, VerifyList)
                .SetupWithVerification(call => call.GetRelegatedTeams(), new Team[]
                {
                    new Team("Relegated Team 1"),
                    new Team("Relegated Team 2")
                });

            MockRepository.CreateMockOf<ITeamRepository>(Container, VerifyList)
                .SetupGetTeamsFromLeague();

            // Act 
            // Assert
            var command = Container.Resolve<ShowRelegatedTeams>(); // Resolves the command with all its dependencies
            command.HandleArguments(CommandArguments.FromArray(new[] { CommandArguments.CommandFullArgName, "ShowChampion" }));
            command.Run();
        }
    }
}