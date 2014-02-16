using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace Football.Repository
{
    /// <summary>
    /// Uses the html agility pack to read out the contents of the required html content
    /// </summary>
    public class PremierLeagueHtmlStrategy : ITeamRepository
    {
        private readonly string _htmlContent;
        private List<Team> _teams;

        public PremierLeagueHtmlStrategy(string htmlContent)
        {
            _htmlContent = htmlContent;
        }

        public IEnumerable<Team> GetTeamsFromLeague()
        {
            // Caching - the teams are already in memory
            if (_teams != null)
                return _teams;

            if (_htmlContent.IsNullOrEmpty())
                throw new HtmlFormatException();

            _teams = new List<Team>();
            
            var document = new HtmlDocument();
            document.LoadHtml(_htmlContent);

            var teamRowElements = document.DocumentNode.SelectNodes("//table[@class='leagueTable']//tbody//tr");

            if (teamRowElements == null)
                throw new HtmlFormatException();
            
            foreach (var row in teamRowElements)
            {
                var columns = row.Elements("td").ToList();
                if (columns.Count != 12)
                    continue;

                _teams.Add(new Team(columns.ElementAt(3).InnerText)
                {
                    Position = int.Parse(columns.ElementAt(0).InnerText),
                    GoalsFor = int.Parse(columns.ElementAt(8).InnerText),
                    GoalsAgainst = int.Parse(columns.ElementAt(9).InnerText),
                    Points = int.Parse(columns.ElementAt(11).InnerText)
                });
            }

            return _teams;
        }

        public void Save(Team team)
        { }
    }
}
