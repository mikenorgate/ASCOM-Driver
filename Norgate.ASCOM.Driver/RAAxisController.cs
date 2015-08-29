using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using ASCOM.DeviceInterface;

namespace ASCOM.Norgate
{
    public class RAAxisController
    {
        private double _startTime;
        private readonly ITelescopeV3 _telescope;
        private double _slewRate;
        private double _previousSlew;
        //Used to lock around the setting of _startTime, _slewRate and _previousSlew
        private readonly object _lockObject = new object();

        public RAAxisController(ITelescopeV3 telescope)
        {
            _previousSlew = 0;
            _slewRate = 1;
            _telescope = telescope;
            _startTime = _telescope.SiderealTime;
        }

        public double Position
        {
            get
            {

                //Gone past end of day
                if (!(_telescope.SiderealTime < _startTime))
                    return (_slewRate * (_telescope.SiderealTime - _startTime)) + _previousSlew;

                lock (_lockObject)
                {
                    _previousSlew = (_slewRate * (Constants.HOURS_PER_SIDEREAL_DAY - _startTime)) + _previousSlew;
                    _startTime = 0;
                }

                return (_slewRate * (_telescope.SiderealTime - _startTime)) + _previousSlew;

            }
            set
            {
                lock (_lockObject)
                {
                    _previousSlew = value;
                    _startTime = _telescope.SiderealTime;
                }
            }
        }

        public double SlewRate
        {
            get { return _slewRate; }
            set
            {
                lock (_lockObject)
                {
                    _previousSlew = Position;
                    _startTime = _telescope.SiderealTime;
                    _slewRate = value;
                }
            }
        }
    }
}
