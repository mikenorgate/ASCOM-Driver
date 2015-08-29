using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASCOM.DeviceInterface;

namespace Norgate.ASCOM.Driver.UnitTests
{
    class FakeTelescope : ITelescopeV3
    {
        public void SetupDialog()
        {
            throw new NotImplementedException();
        }

        public string Action(string ActionName, string ActionParameters)
        {
            throw new NotImplementedException();
        }

        public void CommandBlind(string Command, bool Raw = false)
        {
            throw new NotImplementedException();
        }

        public bool CommandBool(string Command, bool Raw = false)
        {
            throw new NotImplementedException();
        }

        public string CommandString(string Command, bool Raw = false)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void AbortSlew()
        {
            throw new NotImplementedException();
        }

        public IAxisRates AxisRates(TelescopeAxes Axis)
        {
            throw new NotImplementedException();
        }

        public bool CanMoveAxis(TelescopeAxes Axis)
        {
            throw new NotImplementedException();
        }

        public PierSide DestinationSideOfPier(double RightAscension, double Declination)
        {
            throw new NotImplementedException();
        }

        public void FindHome()
        {
            throw new NotImplementedException();
        }

        public void MoveAxis(TelescopeAxes Axis, double Rate)
        {
            throw new NotImplementedException();
        }

        public void Park()
        {
            throw new NotImplementedException();
        }

        public void PulseGuide(GuideDirections Direction, int Duration)
        {
            throw new NotImplementedException();
        }

        public void SetPark()
        {
            throw new NotImplementedException();
        }

        public void SlewToAltAz(double Azimuth, double Altitude)
        {
            throw new NotImplementedException();
        }

        public void SlewToAltAzAsync(double Azimuth, double Altitude)
        {
            throw new NotImplementedException();
        }

        public void SlewToCoordinates(double RightAscension, double Declination)
        {
            throw new NotImplementedException();
        }

        public void SlewToCoordinatesAsync(double RightAscension, double Declination)
        {
            throw new NotImplementedException();
        }

        public void SlewToTarget()
        {
            throw new NotImplementedException();
        }

        public void SlewToTargetAsync()
        {
            throw new NotImplementedException();
        }

        public void SyncToAltAz(double Azimuth, double Altitude)
        {
            throw new NotImplementedException();
        }

        public void SyncToCoordinates(double RightAscension, double Declination)
        {
            throw new NotImplementedException();
        }

        public void SyncToTarget()
        {
            throw new NotImplementedException();
        }

        public void Unpark()
        {
            throw new NotImplementedException();
        }

        public bool Connected { get; set; }
        public string Description { get; }
        public string DriverInfo { get; }
        public string DriverVersion { get; }
        public short InterfaceVersion { get; }
        public string Name { get; }
        public ArrayList SupportedActions { get; }
        public AlignmentModes AlignmentMode { get; }
        public double Altitude { get; }
        public double ApertureArea { get; }
        public double ApertureDiameter { get; }
        public bool AtHome { get; }
        public bool AtPark { get; }
        public double Azimuth { get; }
        public bool CanFindHome { get; }
        public bool CanPark { get; }
        public bool CanPulseGuide { get; }
        public bool CanSetDeclinationRate { get; }
        public bool CanSetGuideRates { get; }
        public bool CanSetPark { get; }
        public bool CanSetPierSide { get; }
        public bool CanSetRightAscensionRate { get; }
        public bool CanSetTracking { get; }
        public bool CanSlew { get; }
        public bool CanSlewAltAz { get; }
        public bool CanSlewAltAzAsync { get; }
        public bool CanSlewAsync { get; }
        public bool CanSync { get; }
        public bool CanSyncAltAz { get; }
        public bool CanUnpark { get; }
        public double Declination { get; }
        public double DeclinationRate { get; set; }
        public bool DoesRefraction { get; set; }
        public EquatorialCoordinateType EquatorialSystem { get; }
        public double FocalLength { get; }
        public double GuideRateDeclination { get; set; }
        public double GuideRateRightAscension { get; set; }
        public bool IsPulseGuiding { get; }
        public double RightAscension { get; }
        public double RightAscensionRate { get; set; }
        public PierSide SideOfPier { get; set; }
        public double SiderealTime { get; set; }
        public double SiteElevation { get; set; }
        public double SiteLatitude { get; set; }
        public double SiteLongitude { get; set; }
        public bool Slewing { get; }
        public short SlewSettleTime { get; set; }
        public double TargetDeclination { get; set; }
        public double TargetRightAscension { get; set; }
        public bool Tracking { get; set; }
        public DriveRates TrackingRate { get; set; }
        public ITrackingRates TrackingRates { get; }
        public DateTime UTCDate { get; set; }
    }
}
