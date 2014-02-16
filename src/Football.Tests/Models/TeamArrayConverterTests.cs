using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Football.Tests.Models
{
    [TestClass]
    public class TeamArrayConverterTests
    {
        [TestMethod]
        public void Convert_ByNotPassingStringArray_ThrowsInvalidCastException()
        {
            // Arrange
            object[] objects = new object[0];

            // Act and Assert
            TeamArrayConverter converter = new TeamArrayConverter();
            Expect.Exception<InvalidCastException>(() => converter.ConvertFrom(objects));
        }

        [TestMethod]
        public void Convert_ByPassingEmptyArray_ThrowsIndexOutOfRangeException()
        {
            // Arrange
            string[] empty = new string[0];

            // Act and Assert
            TeamArrayConverter converter = new TeamArrayConverter();
            Expect.Exception<IndexOutOfRangeException>(() => converter.ConvertFrom(empty));
        }

        [TestMethod]
        public void Convert_ValidStringArray_ReturnsTeamObjectInstance()
        {
            // Arrange
            // This is an array structure that we expect to come out from the CSV file
            string[] propertiesRepresentingTeam = { "1", "Arsenal", "38", "26", "9", "3", "79", "-", "36", "87" };

            // Act
            TeamArrayConverter converter = new TeamArrayConverter();
            var result = converter.ConvertFrom(propertiesRepresentingTeam);

            // Assert
            result.IsInstanceOf<Team>();
            Team teamResult = (Team) result;
            teamResult.Name.IsEqualTo("Arsenal");
            teamResult.GoalsAgainst.IsEqualTo(36);
            teamResult.GoalsFor.IsEqualTo(79);
        }

        [TestMethod]
        public void Convert_GoalsForPropertyIsNotANumber_ThrowsFormatException()
        {
            // Arrange
            string[] propertiesRepresentingTeam = { "1" , "Arsenal", "38", "26", "9", "3", "invalid", "-", "invalid", "87" };

            // Act and Assert exception
            TeamArrayConverter converter = new TeamArrayConverter();
            
            Expect.Exception<FormatException>(() =>  converter.ConvertFrom(propertiesRepresentingTeam));
        }
    }
}