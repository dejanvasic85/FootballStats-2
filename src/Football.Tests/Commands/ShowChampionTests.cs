using Football.Commands;
using Football.Repository;
using Football.Services;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Football.Tests.Commands
{
    [TestClass]
    public class ShowChampionTests : BaseTest
    {
        [TestMethod]
        public void Go_LogsTheChampion_VerificationSuccessful()
        {
            // Arrange
            MockRepository.CreateMockOf<ILogService>(Container, VerifyList)
                .SetupWithVerification(call => call.Log(It.Is<string>(val => val == "And the champion is: Arsenal!!!!!")))
                .SetupWithVerification(call => call.Info(It.IsAny<string>()));

            MockRepository.CreateMockOf<ITeamService>(Container, VerifyList)
                .SetupWithVerification(call => call.GetChampion(), new Team("Arsenal"));

            MockRepository.CreateMockOf<ITeamRepository>(Container, VerifyList)
                .SetupGetTeamsFromLeague();

            // Act 
            // Assert
            var command = Container.Resolve<ShowChampion>(); // Resolves the command with all its dependencies
            command.HandleArguments(CommandArguments.FromArray(new[]{CommandArguments.CommandFullArgName, "ShowChampion"}));
            command.Run();
        }
    }
}