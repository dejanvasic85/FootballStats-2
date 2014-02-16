using System;
using System.Collections.Generic;
using Football.Commands;
using Football.Repository;
using Football.Services;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Football.Tests.Commands
{
    [TestClass]
    public class ShowAllTeamsTests : BaseTest
    {
        [TestMethod]
        public void Go_LogsAllTeams_VerificationSuccessful()
        {
            var teams = new List<Team>
            {
                new Team("Arsenal"),             
                new Team("Manchester United"),  
                new Team("Manchester City")
            };

            // Arrange
            MockRepository.CreateMockOf<ILogService>(Container, VerifyList)
                .SetupWithVerification(call => call.Log(It.IsAny<string>()))
                .SetupWithVerification(call => call.Info(It.IsAny<string>()));

            MockRepository.CreateMockOf<ITeamRepository>(Container, VerifyList)
                .SetupWithVerification(call => call.GetTeamsFromLeague(), result: teams);

            // Act 
            // Assert (verification based)
            var command = Container.Resolve<ShowAllTeams>();
            command.HandleArguments(CommandArguments.FromArray(new[]
            {
                CommandArguments.CommandFullArgName, "ShowAllTeamsTest",
                "-ShowGoals", "true"
            }));
            command.Run();
        }
    }
}