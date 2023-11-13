using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class DomesticAssistant : Robot
    {
        public const int domesticAssistantBatteryCapacity = 20_000;
        public const int domesticAssistantConvertionCapacityIndex = 2_000;

        public DomesticAssistant(string model) : base(model, domesticAssistantBatteryCapacity, domesticAssistantConvertionCapacityIndex) { }
    }
}
