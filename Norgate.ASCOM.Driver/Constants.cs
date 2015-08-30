using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASCOM.Norgate
{
    public class Constants
    {
        public const double SECONDS_PER_SIDEREAL_DAY = 86164.0905d;
        public const double HOURS_PER_SIDEREAL_DAY = 23.9344696;
        public const double DEGREES_PER_SECOND = 360d / SECONDS_PER_SIDEREAL_DAY;
        public const double DEGREES_PER_HOUR = 360d / HOURS_PER_SIDEREAL_DAY;
        public const int MAX_RATE_MULTIPLYER = 8;

    }
}
