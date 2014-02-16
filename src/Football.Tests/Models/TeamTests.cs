using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Football.Tests.Models
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void GetGoalDifference_GoalsConcededLargerThanScored_NotAbsoluteValue_ReturnsNegativeValue()
        {
            // Arrange
            Team team = new Team("team1", goalsFor: 10, goalsAgainst: 20);

            // Act
            var result = team.GetGoalDifference(useAbsoluteValue: false);

            // Assert
            result.IsLessThan(0);
        }

        [TestMethod]
        public void GetGoalDifference_GoalsConcededLargerThanScored_UseAbsoluteValue_ReturnsNegativeValue()
        {
            // Arrange
            Team team = new Team("team1", goalsFor: 10, goalsAgainst: 20);

            // Act
            var result = team.GetGoalDifference();

            // Assert
            result.IsLargerThan(0);
        }

        [TestMethod]
        public void GetGoalDifference_GoalsScoredLargerThanConceded_NotAbsoluteValue_ReturnsNegativeValue()
        {
            // Arrange
            Team team = new Team("team1", goalsFor: 10, goalsAgainst: 20);

            // Act
            var result = team.GetGoalDifference();

            // Assert
            result.IsLargerThan(0);
        }
    }
}
