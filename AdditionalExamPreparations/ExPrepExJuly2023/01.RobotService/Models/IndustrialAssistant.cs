using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class IndustrialAssistant : Robot
    {
        public const int industrialAssistantBatteryCapacity = 40_000;
        public const int industrialAssistantConvertionCapacityIndex = 5_000;

        public IndustrialAssistant(string model) : base(model, industrialAssistantBatteryCapacity, industrialAssistantConvertionCapacityIndex) { }
    }
}
