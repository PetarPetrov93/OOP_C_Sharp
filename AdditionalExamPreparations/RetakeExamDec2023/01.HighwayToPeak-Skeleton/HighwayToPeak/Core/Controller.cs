using HighwayToPeak.Core.Contracts;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using HighwayToPeak.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighwayToPeak.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IPeak> peaks;
        private readonly IRepository<IClimber> climbers;
        private BaseCamp baseCamp;

        public Controller()
        {
            peaks = new PeakRepository();
            climbers = new ClimberRepository();
            baseCamp = new BaseCamp();
        }
        public string AddPeak(string name, int elevation, string difficultyLevel)
        {
            if (peaks.Get(name) is not null)
            {
                return $"{name} is already added as a valid mountain destination.";
            }

            if (difficultyLevel != "Extreme" && difficultyLevel != "Hard" && difficultyLevel != "Moderate")
            {
                return $"{difficultyLevel} peaks are not allowed for international climbers.";
            }

            IPeak peak = new Peak(name, elevation, difficultyLevel);
            peaks.Add(peak);
            return $"{name} is allowed for international climbing. See details in {nameof(PeakRepository)}.";
        }

        public string NewClimberAtCamp(string name, bool isOxygenUsed)
        {
            if (climbers.Get(name) is not null)
            {
                return $"{name} is a participant in {nameof(ClimberRepository)} and cannot be duplicated.";
            }

            IClimber climber = null;

            if (isOxygenUsed)
            {
               climber = new OxygenClimber(name);
            }
            else
            {
                climber = new NaturalClimber(name);
            }

            climbers.Add(climber);
            baseCamp.ArriveAtCamp(name);
            return $"{name} has arrived at the BaseCamp and will wait for the best conditions.";
        }

        public string AttackPeak(string climberName, string peakName)
        {
            IClimber climber = climbers.Get(climberName);
            IPeak peak = peaks.Get(peakName);

            if (climber is null)
            {
                return $"Climber - {climberName}, has not arrived at the BaseCamp yet.";
            }

            if (peak is null)
            {
                return $"{peakName} is not allowed for international climbing.";
            }

            if (!baseCamp.Residents.Any(climber => climber == climberName))
            {
                return $"{climberName} not found for gearing and instructions. The attack of {peakName} will be postponed.";
            }

            if (peak.DifficultyLevel == "Extreme" && climber.GetType().Name == nameof(NaturalClimber))
            {
                return $"{climberName} does not cover the requirements for climbing {peakName}.";
            }

            baseCamp.LeaveCamp(climberName);
            climber.Climb(peak);

            if (climber.Stamina == 0)
            {
                return $"{climberName} did not return to BaseCamp.";
            }
            else
            {
                baseCamp.ArriveAtCamp(climberName);
                return $"{climberName} successfully conquered {peakName} and returned to BaseCamp.";
            }
        }

        public string CampRecovery(string climberName, int daysToRecover)
        {
            if (!baseCamp.Residents.Any(resident => resident == climberName))
            {
                return $"{climberName} not found at the BaseCamp.";
            }

            IClimber climber = climbers.Get(climberName);

            if (climber.Stamina == 10)
            {
                return $"{climberName} has no need of recovery.";
            }

            climber.Rest(daysToRecover);
            return $"{climberName} has been recovering for {daysToRecover} days and is ready to attack the mountain.";
        }

        public string BaseCampReport()
        {
            if (baseCamp.Residents.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                IClimber climber = null;

                sb.AppendLine("BaseCamp residents:");
                foreach (string resident in baseCamp.Residents)
                {
                    climber = climbers.Get(resident);

                    sb.AppendLine($"Name: {climber.Name}, Stamina: {climber.Stamina}, Count of Conquered Peaks: {climber.ConqueredPeaks.Count}");
                }
                return sb.ToString().TrimEnd();
            }
            return "BaseCamp is currently empty.";
        }

        public string OverallStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***Highway-To-Peak***");

            foreach (IClimber climber in climbers.All.OrderByDescending(c => c.ConqueredPeaks.Count).ThenBy(c => c.Name))
            {
                sb.AppendLine(climber.ToString());

                ICollection<IPeak> concquered = new List<IPeak>();

                foreach (string peak in climber.ConqueredPeaks)
                {
                    concquered.Add(peaks.Get(peak));
                }
                foreach (IPeak peakConquered in concquered.OrderByDescending(p => p.Elevation))
                {
                    sb.AppendLine(peakConquered.ToString());
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
