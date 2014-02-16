using Football.Commands;
using Football.Repository;
using Football.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Moq;

namespace Football.Tests.Commands
{
    [TestClass]
    public class ShowTeamWithLeastGoalDifferenceTests : BaseTest
    {
        [TestMethod]
        public void Go_LogsTheTeamWithLeastGoalDifference_VerificationSuccessful()
        {
            // Arrange
            MockRepository.CreateMockOf<ILogService>(Container, VerifyList)
                .SetupWithVerification(call => call.Log(It.Is<string>(val=> val.Equals("[10] Team 1 - Goal Difference: 2"))))
                .SetupWithVerification(call => call.Info(It.IsAny<string>()));

            MockRepository.CreateMockOf<ITeamService>(Container, VerifyList)
                .SetupWithVerification(call => call.GetTeamWithLeastGoalDifference(), new Team("Team 1", 1, 3) { Position = 10});

            MockRepository.CreateMockOf<ITeamRepository>(Container, VerifyList)
                .SetupGetTeamsFromLeague();

            // Act 
            // Assert
            var command = Container.Resolve<ShowTeamWithLeastGoalDifference>(); // Resolves the command with all its dependencies
            command.HandleArguments(CommandArguments.FromArray(new[] { CommandArguments.CommandFullArgName, "ShowChampion" }));
            command.Run();
        }
    }
}