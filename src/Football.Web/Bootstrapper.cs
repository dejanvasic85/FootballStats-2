using Football.Repository;
using Football.Services;
using Microsoft.Practices.Unity;
using System.Net;
using System.Web.Mvc;
using Unity.Mvc4;

namespace Football.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<ITeamService, TeamService>()

                // Use the web client to fetch the html content and create instance of the Premier League Scraper
                .RegisterType<ITeamRepository, PremierLeagueHtmlStrategy>(new ContainerControlledLifetimeManager(), 
                new InjectionFactory(c =>
                {
                    var client = new WebClient();
                    var content = client.DownloadString("http://www.premierleague.com/en-gb/matchday/league-table.html");
                    return new PremierLeagueHtmlStrategy(content);
                }));
        }
    }
}