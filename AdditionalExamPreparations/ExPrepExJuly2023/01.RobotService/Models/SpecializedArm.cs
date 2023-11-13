using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Models
{
    public class SpecializedArm : Supplement
    {
        private const int specializedArmInterfaceStandard = 10045;
        private const int specializedArmBatteryUsage = 10_000;
        public SpecializedArm() : base(specializedArmInterfaceStandard, specializedArmBatteryUsage) { }
    }
}
