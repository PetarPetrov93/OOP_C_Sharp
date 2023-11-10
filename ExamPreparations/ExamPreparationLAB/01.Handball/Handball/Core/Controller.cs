using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Core
{
    public class Controller : IController
    {
        private readonly PlayerRepository players;
        private readonly TeamRepository teams;
        public Controller()
        {
            players = new PlayerRepository();
            teams = new TeamRepository();
        }
        public string LeagueStandings()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***League Standings***");
            foreach (var team in teams.Models.OrderByDescending(t => t.PointsEarned).ThenByDescending(t => t.OverallRating).ThenBy(t => t.Name))
            {
                sb.AppendLine(team.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string NewContract(string playerName, string teamName)
        {
            if (!players.ExistsModel(playerName))
            {
                return string.Format(OutputMessages.PlayerNotExisting, playerName, players.GetType().Name);
            }
            if (!teams.ExistsModel(teamName))
            {
                return string.Format(OutputMessages.TeamNotExisting, teamName, teams.GetType().Name);
            }
            if (players.GetModel(playerName).Team != null)
            {
                return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, players.GetModel(playerName).Team);
            }
            players.GetModel(playerName).JoinTeam(teamName);
            teams.GetModel(teamName).SignContract(players.GetModel(playerName));
            return string.Format(OutputMessages.SignContract, playerName, teamName);
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            if (teams.GetModel(firstTeamName).OverallRating > teams.GetModel(secondTeamName).OverallRating)
            {
                teams.GetModel(firstTeamName).Win();
                teams.GetModel(secondTeamName).Lose();
                return string.Format(OutputMessages.GameHasWinner, firstTeamName, secondTeamName);
            }
            if (teams.GetModel(firstTeamName).OverallRating < teams.GetModel(secondTeamName).OverallRating)
            {
                teams.GetModel(secondTeamName).Win();
                teams.GetModel(firstTeamName).Lose();
                return string.Format(OutputMessages.GameHasWinner, secondTeamName, firstTeamName);
            }
            teams.GetModel(firstTeamName).Draw();
            teams.GetModel(secondTeamName).Draw();
            return string.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);

        }

        public string NewPlayer(string typeName, string name)
        {
            if (typeName != "Goalkeeper" && typeName != "CenterBack" && typeName != "ForwardWing")
            {
                return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }
            if (players.ExistsModel(name))
            {
                return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, players.GetType().Name, players.GetModel(name).GetType().Name);
            }
            IPlayer player = null;
            if (typeName == "Goalkeeper")
            {
                player = new Goalkeeper(name);
            }
            else if (typeName == "CenterBack")
            {
                player = new CenterBack(name);
            }
            else if (typeName == "ForwardWing")
            {
                player = new ForwardWing(name);
            }
            players.AddModel(player);
            return string.Format(OutputMessages.PlayerAddedSuccessfully, name);
        }

        public string NewTeam(string name)
        {
            if (teams.ExistsModel(name))
            {
                return string.Format(OutputMessages.TeamAlreadyExists, name, teams.GetType().Name);
            }

            teams.AddModel(new Team(name));
            return string.Format(OutputMessages.TeamSuccessfullyAdded, name, teams.GetType().Name);
        }

        public string PlayerStatistics(string teamName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"***{teamName}***");
            foreach (var player in teams.GetModel(teamName).Players.OrderByDescending(p => p.Rating).ThenBy(p => p.Name))
            {
                sb.AppendLine(player.ToString());
            }
            return sb.ToString().TrimEnd();
        }  
    }
}
