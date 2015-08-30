using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASCOM.DeviceInterface;

namespace ASCOM.Norgate
{
    public class DecAxisController : IAxisController
    {
        private const double MAX_ANGLE = 90;
        private const double SLEW_RATE = 8;

        private double _startTime;
        private readonly ITelescopeV3 _telescope;
        private double _slewRate;
        private double _previousSlew;
        //Used to lock around the setting of _startTime, _slewRate and _previousSlew
        private readonly object _lockObject = new object();
        private bool _tracking;
        private bool _slewing;
        private bool _started;

        public DecAxisController(ITelescopeV3 telescope)
        {
            _previousSlew = 0;
            _slewRate = 0;
            _telescope = telescope;
            _tracking = false;
            _slewing = false;
        }

        public double Position
        {
            get
            {

                //Gone past end of day
                if ((_telescope.SiderealTime < _startTime))
                {
                    lock (_lockObject)
                    {
                        _previousSlew = (_slewRate * ((Constants.HOURS_PER_SIDEREAL_DAY - _startTime)) * Constants.DEGREES_PER_HOUR) + _previousSlew;
                        _startTime = 0;
                    }
                }

                var value = (_slewRate * ((_telescope.SiderealTime - _startTime) * Constants.DEGREES_PER_HOUR)) + _previousSlew;

                if (Math.Abs(value) > MAX_ANGLE)
                {
                    _previousSlew = value > 0 ? MAX_ANGLE : -MAX_ANGLE;
                    _startTime = _telescope.SiderealTime;
                    _slewRate = 0;
                    _slewing = false;
                    value = _previousSlew;
                }

                return value;
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
            private set
            {
                lock (_lockObject)
                {
                    _previousSlew = Position;
                    _startTime = _telescope.SiderealTime;
                    _slewRate = value;
                    _slewing = value != 0d;
                }
            }
        }

        public void Connect()
        {
            //This axis does not support tracking
            _started = true;
        }

        public void Disconnect()
        {
            _previousSlew = Position;
            SlewRate = 0;
            
            _started = false;
        }

        public Task Slew(Orientation orientation, TimeSpan time)
        {
            if (!_started)
            {
                throw new AxisNotStartedException();
            }

            StartSlew(orientation);
            var endTime = _startTime + time.TotalHours;

            return Task.Factory.StartNew(() =>
            {
                while (_telescope.SiderealTime < endTime && Math.Abs(Position) < MAX_ANGLE && _slewing) ;
                StopSlew();
            });
        }

        public void StartSlew(Orientation orientation)
        {
            if (!_started)
            {
                throw new AxisNotStartedException();
            }

            SlewRate = orientation == Orientation.Plus ? SLEW_RATE : -SLEW_RATE;
        }

        public void StopSlew()
        {
            if (!_started)
            {
                throw new AxisNotStartedException();
            }

            SlewRate = 0;
        }
    }
}
