using Football.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Football.Tests.Repository
{
    [TestClass]
    public class PremierLeagueHtmlStrategyTests
    {
        [TestMethod]
        [DeploymentItem("Artefacts\\PremierLeagueValid.html")]
        public void GetTeamsFromLeague_ValidHtmlStructure_ReturnsTeams()
        {
            // Arrange
            // Fetch the content from the flat file 
            string htmlFromFile = File.ReadAllText("PremierLeagueValid.html");

            // Act
            ITeamRepository htmlStrategy = new PremierLeagueHtmlStrategy(htmlFromFile);
            var result = htmlStrategy.GetTeamsFromLeague();

            // Assert
            result.IsInstanceOf<List<Team>>();
            result.ElementAt(0).Name.IsEqualTo("Chelsea");
            result.Count().IsEqualTo(20);
        }

        [TestMethod]
        [DeploymentItem("Artefacts\\PremierLeagueBad.html")]
        public void GetTeamsFromLeague_BadHtml_ThrowsHtmlException()
        {
            // Arrange
            // Fetch the content from the flat file 
            string htmlFromFile = File.ReadAllText("PremierLeagueBad.html");

            // Act 
            // Assert
            ITeamRepository htmlStrategy = new PremierLeagueHtmlStrategy(htmlFromFile);
            Expect.Exception<HtmlFormatException>( () => htmlStrategy.GetTeamsFromLeague() );

        }

        [TestMethod]
        [DeploymentItem("Artefacts\\PremierLeagueEmpty.html")]
        public void GetTeamsFromLeague_EmptyHtml_Throws()
        {
            // Arrange
            // Fetch the content from the flat file 
            string htmlFromFile = File.ReadAllText("PremierLeagueEmpty.html");

            // Act 
            // Assert
            PremierLeagueHtmlStrategy htmlStrategy = new PremierLeagueHtmlStrategy(htmlFromFile);
            Expect.Exception<HtmlFormatException>(() => htmlStrategy.GetTeamsFromLeague());
        }
    }
}
