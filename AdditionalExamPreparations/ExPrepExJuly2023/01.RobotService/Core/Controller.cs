using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private readonly IRepository<ISupplement> supplements;
        private readonly IRepository<IRobot> robots;
        public Controller()
        {
            supplements = new SupplementRepository();
            robots = new RobotRepository();

        }

        public string CreateRobot(string model, string typeName)
        {
            if (typeName != "DomesticAssistant" && typeName != "IndustrialAssistant")
            {
                return $"Robot type {typeName} cannot be created.";
            }

            IRobot robot = null;

            if (typeName == "DomesticAssistant")
            {
                robot = new DomesticAssistant(model);
            }
            else if (typeName == "IndustrialAssistant")
            {
                robot = new IndustrialAssistant(model);
            }

            robots.AddNew(robot);

            return $"{typeName} {model} is created and added to the RobotRepository.";
        }

        public string CreateSupplement(string typeName)
        {
            if (typeName != "SpecializedArm" && typeName != "LaserRadar")
            {
                return $"{typeName} is not compatible with our robots.";
            }
            ISupplement supplement = null;

            if (typeName == "SpecializedArm")
            {
                supplement = new SpecializedArm();
            }
            else if (typeName == "LaserRadar")
            {
                supplement = new LaserRadar();
            }

            supplements.AddNew(supplement);

            return $"{typeName} is created and added to the SupplementRepository.";
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplementToAdd = supplements.Models().FirstOrDefault(s => s.GetType().Name == supplementTypeName);

            if (robots.Models().Any(r => r.Model == model && !(r.InterfaceStandards.Contains(supplementToAdd.InterfaceStandard))))
            {
                IRobot robotToUpgrade = robots.Models().FirstOrDefault(r => r.Model == model && !(r.InterfaceStandards.Contains(supplementToAdd.InterfaceStandard)));

                robotToUpgrade.InstallSupplement(supplementToAdd);

                supplements.RemoveByName(supplementTypeName);

                return $"{model} is upgraded with {supplementTypeName}.";
            }

            return $"All {model} are already upgraded!";
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            if (!robots.Models().Any(r => r.InterfaceStandards.Contains(intefaceStandard)))
            {
                return $"Unable to perform service, {intefaceStandard} not supported!";
            }

            int batteryLevelSum = 0;

            foreach (IRobot robot in robots.Models().Where(r => r.InterfaceStandards.Contains(intefaceStandard)).OrderByDescending(r => r.BatteryLevel))
            {
                batteryLevelSum += robot.BatteryLevel;
            }

            if (batteryLevelSum < totalPowerNeeded)
            {
                return $"{serviceName} cannot be executed! {totalPowerNeeded - batteryLevelSum} more power needed.";
            }

            int robotsThatTookPartInTheService = 0;

            foreach (IRobot robot in robots.Models().Where(r => r.InterfaceStandards.Contains(intefaceStandard)).OrderByDescending(r => r.BatteryLevel))
            {
                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robotsThatTookPartInTheService++;
                    robot.ExecuteService(totalPowerNeeded);
                    break;
                }
                if (robot.BatteryLevel < totalPowerNeeded)
                {
                    robotsThatTookPartInTheService++;
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                }
            }

            return $"{serviceName} is performed successfully with {robotsThatTookPartInTheService} robots.";
        }

        public string RobotRecovery(string model, int minutes)
        {
            int fedCount = 0;

            foreach (IRobot robot in robots.Models().Where(r =>r.Model == model && r.BatteryLevel / r.BatteryCapacity * 100 < 50))
            {
                robot.Eating(minutes);
                fedCount++;
            }

            return $"Robots fed: {fedCount}";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IRobot robot in robots.Models().OrderByDescending(r => r.BatteryLevel).ThenBy(r => r.BatteryCapacity))
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

    }
}
