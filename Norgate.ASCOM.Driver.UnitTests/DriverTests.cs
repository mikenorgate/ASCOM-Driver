using System;
using ASCOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASCOM.Norgate;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

namespace Norgate.ASCOM.Driver.UnitTests
{
    [TestClass]
    public class DriverTests
    {
        private Telescope telescope;

        [TestInitialize]
        public void Initialise()
        {
            telescope = new Telescope();
        }

        #region AlignmentMode
        [TestMethod]
        public void AlignmentMode_IsGermanPolar()
        {
            Assert.AreEqual(AlignmentModes.algGermanPolar, telescope.AlignmentMode);
        }
        #endregion AlignmentMode

        #region AxisRates
        [TestMethod]
        public void AxisRates_Primary()
        {
            var rates = telescope.AxisRates(TelescopeAxes.axisPrimary);

            Assert.AreEqual(1, rates.Count);
            Assert.AreEqual(0, rates[1].Minimum);
            Assert.AreEqual(Constants.DEGREES_PER_SECOND * Constants.MAX_RATE_MULTIPLYER, rates[1].Maximum);
        }

        [TestMethod]
        public void AxisRates_Secondary()
        {
            var rates = telescope.AxisRates(TelescopeAxes.axisSecondary);

            Assert.AreEqual(1, rates.Count);
            Assert.AreEqual(0, rates[1].Minimum);
            Assert.AreEqual(Constants.DEGREES_PER_SECOND * Constants.MAX_RATE_MULTIPLYER, rates[1].Maximum);
        }

        [TestMethod]
        public void AxisRates_Tertiary()
        {
            var rates = telescope.AxisRates(TelescopeAxes.axisTertiary);

            Assert.AreEqual(0, rates.Count);
        }
        #endregion AxisRates

        #region CanMoveAxis
        [TestMethod]
        public void CanMoveAxis_Primary()
        {
            Assert.IsTrue(telescope.CanMoveAxis(TelescopeAxes.axisPrimary));
        }

        [TestMethod]
        public void CanMoveAxis_Secondary()
        {
            Assert.IsTrue(telescope.CanMoveAxis(TelescopeAxes.axisSecondary));
        }

        [TestMethod]
        public void CanMoveAxis_Tertiary()
        {
            Assert.IsFalse(telescope.CanMoveAxis(TelescopeAxes.axisTertiary));
        }
        #endregion CanMoveAxis

        #region EquatorialSystem
        [TestMethod]
        public void EquatorialSystem_IsLocalTopocentric()
        {
            Assert.AreEqual(EquatorialCoordinateType.equLocalTopocentric, telescope.EquatorialSystem);
        }
        #endregion EquatorialSystem

        #region TrackingRate
        [TestMethod]
        public void TrackingRateGet_Sidereal()
        {
            Assert.AreEqual(DriveRates.driveSidereal, telescope.TrackingRate);
        }

        [TestMethod]
        [ExpectedException(typeof(PropertyNotImplementedException))]
        public void TrackingRateSet_ThrowsPropertyNotImplemented()
        {
            telescope.TrackingRate = DriveRates.driveKing;
        }
        #endregion TrackingRate

        #region TrackingRates
        [TestMethod]
        public void TrackingRates_ContainsOnlySidereal()
        {
            var rates = telescope.TrackingRates;

            Assert.AreEqual(1, rates.Count);
            Assert.AreEqual(DriveRates.driveSidereal, rates[1]);
        }
        #endregion TrackingRates
    }
}
