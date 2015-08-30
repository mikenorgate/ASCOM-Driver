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
        public string Description { get; set; }
        public string DriverInfo { get; set; }
        public string DriverVersion { get; set; }
        public short InterfaceVersion { get; set; }
        public string Name { get; set; }
        public ArrayList SupportedActions { get; set; }
        public AlignmentModes AlignmentMode { get; set; }
        public double Altitude { get; set; }
        public double ApertureArea { get; set; }
        public double ApertureDiameter { get; set; }
        public bool AtHome { get; set; }
        public bool AtPark { get; set; }
        public double Azimuth { get; set; }
        public bool CanFindHome { get; set; }
        public bool CanPark { get; set; }
        public bool CanPulseGuide { get; set; }
        public bool CanSetDeclinationRate { get; set; }
        public bool CanSetGuideRates { get; set; }
        public bool CanSetPark { get; set; }
        public bool CanSetPierSide { get; set; }
        public bool CanSetRightAscensionRate { get; set; }
        public bool CanSetTracking { get; set; }
        public bool CanSlew { get; set; }
        public bool CanSlewAltAz { get; set; }
        public bool CanSlewAltAzAsync { get; set; }
        public bool CanSlewAsync { get; set; }
        public bool CanSync { get; set; }
        public bool CanSyncAltAz { get; set; }
        public bool CanUnpark { get; set; }
        public double Declination { get; set; }
        public double DeclinationRate { get; set; }
        public bool DoesRefraction { get; set; }
        public EquatorialCoordinateType EquatorialSystem { get; set; }
        public double FocalLength { get; set; }
        public double GuideRateDeclination { get; set; }
        public double GuideRateRightAscension { get; set; }
        public bool IsPulseGuiding { get; set; }
        public double RightAscension { get; set; }
        public double RightAscensionRate { get; set; }
        public PierSide SideOfPier { get; set; }
        public double SiderealTime { get; set; }
        public double SiteElevation { get; set; }
        public double SiteLatitude { get; set; }
        public double SiteLongitude { get; set; }
        public bool Slewing { get; set; }
        public short SlewSettleTime { get; set; }
        public double TargetDeclination { get; set; }
        public double TargetRightAscension { get; set; }
        public bool Tracking { get; set; }
        public DriveRates TrackingRate { get; set; }
        public ITrackingRates TrackingRates { get; set; }
        public DateTime UTCDate { get; set; }
    }
}
