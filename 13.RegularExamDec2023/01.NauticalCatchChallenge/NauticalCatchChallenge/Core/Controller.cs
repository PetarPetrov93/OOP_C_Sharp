using NauticalCatchChallenge.Core.Contracts;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Models.Diver;
using NauticalCatchChallenge.Models.Fish;
using NauticalCatchChallenge.Repositories;
using NauticalCatchChallenge.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IDiver> divers;
        private readonly IRepository<IFish> fish;

        public Controller()
        {
            divers = new DiverRepository();
            fish = new FishRepository();
        }
        public string DiveIntoCompetition(string diverType, string diverName)
        {
            if (diverType != nameof(FreeDiver) && diverType != nameof(ScubaDiver))
            {
                return $"{diverType} is not allowed in our competition.";
            }

            if (divers.Models.Any(d => d.Name == diverName))
            {
                return $"{diverName} is already a participant -> {nameof(DiverRepository)}.";
            }

            IDiver diver = null;

            if (diverType == nameof(FreeDiver))
            {
                diver = new FreeDiver(diverName);
            }
            else if (diverType == nameof(ScubaDiver))
            {
                diver = new ScubaDiver(diverName);
            }

            divers.AddModel(diver);

            return $"{diverName} is successfully registered for the competition -> {nameof(DiverRepository)}.";
        }

        public string SwimIntoCompetition(string fishType, string fishName, double points)
        {
            if (fishType != nameof(ReefFish) && fishType != nameof(PredatoryFish) && fishType != nameof(DeepSeaFish))
            {
                return $"{fishType} is forbidden for chasing in our competition.";
            }

            if (fish.Models.Any(f => f.Name == fishName))
            {
                return $"{fishName} is already allowed -> {nameof(FishRepository)}.";
            }

            IFish newFish = null;

            if (fishType == nameof(ReefFish))
            {
                newFish = new ReefFish(fishName, points);
            }
            else if (fishType == nameof(PredatoryFish))
            {
                newFish = new PredatoryFish(fishName, points);
            }
            else if (fishType == nameof(DeepSeaFish))
            {
                newFish = new DeepSeaFish(fishName, points);
            }

            fish.AddModel(newFish);

            return $"{fishName} is allowed for chasing.";
        }

        public string ChaseFish(string diverName, string fishName, bool isLucky)
        {
            IDiver diver = divers.GetModel(diverName);
            IFish fishToChase = fish.GetModel(fishName);

            if (diver is null)
            {
                return $"{nameof(DiverRepository)} has no {diverName} registered for the competition.";
            }

            if (fishToChase is null)
            {
                return $"{fishName} is not allowed to be caught in this competition.";
            }

            if (diver.HasHealthIssues == true)
            {
                return $"{diverName} will not be allowed to dive, due to health issues.";
            }

            if (diver.OxygenLevel < fishToChase.TimeToCatch) // I think the Miss method should be invoked here.
            {
                diver.Miss(fishToChase.TimeToCatch);

                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }

                return $"{diverName} missed a good {fishName}.";
            }

            if (diver.OxygenLevel == fishToChase.TimeToCatch && isLucky == true)
            {
                diver.Hit(fishToChase);

                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }

                return $"{diverName} hits a {fishToChase.Points}pt. {fishName}.";
            }
            else if (diver.OxygenLevel == fishToChase.TimeToCatch && isLucky == false)
            {
                diver.Miss(fishToChase.TimeToCatch);

                if (diver.OxygenLevel == 0)
                {
                    diver.UpdateHealthStatus();
                }

                return $"{diverName} missed a good {fishName}.";
            }
            else
            {
                diver.Hit(fishToChase);

                //No need to invoke the UpdateHealthStatus() here as OxygenLevel will not drop below 0

                return $"{diverName} hits a {fishToChase.Points}pt. {fishName}.";
            }

        }

        public string HealthRecovery()
        {
            int count = 0;
            foreach (IDiver diver in divers.Models.Where(d => d.HasHealthIssues == true))
            {
                diver.UpdateHealthStatus();
                diver.RenewOxy();
                count++;
            }
            return $"Divers recovered: {count}";
        }

        public string DiverCatchReport(string diverName)
        {
            IDiver diver = divers.GetModel(diverName);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(diver.ToString());
            sb.AppendLine("Catch Report:");

            foreach (string caughtFish in diver.Catch)
            {
                sb.AppendLine(fish.GetModel(caughtFish).ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string CompetitionStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("**Nautical-Catch-Challenge**");

            foreach (IDiver diver in divers.Models.Where(d => d.HasHealthIssues == false).OrderByDescending(d => d.CompetitionPoints).ThenByDescending(d => d.Catch.Count).ThenBy(d => d.Name))
            {
                sb.AppendLine(diver.ToString());
            }

            return sb.ToString().TrimEnd();
        }

    }
}
