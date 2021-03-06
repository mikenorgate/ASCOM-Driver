﻿using System;
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

        #region RightAscension
        [TestMethod]
        public void RightAscension_ReturnsRAFromAxisController()
        {
            telescope.RAAxisController.Position = 10.345d;

            Assert.AreEqual(telescope.RAAxisController.Position, telescope.RightAscension);
        }
        #endregion RightAscension

        #region RightAscensionRate
        [TestMethod]
        public void GetRightAscensionRate_Returns0()
        {
            Assert.AreEqual(0, telescope.RightAscensionRate);
        }

        [TestMethod]
        [ExpectedException(typeof(PropertyNotImplementedException))]
        public void SetRightAscensionRate_ThrowsNotImplemented()
        {
            telescope.RightAscensionRate = 10;
        }
        #endregion RightAscensionRate

        #region CanSetRightAscensionRate
        [TestMethod]
        public void CanSetRightAscensionRate_ReturnsFalse()
        {
            Assert.IsFalse(telescope.CanSetRightAscensionRate);
        }
        #endregion CanSetRightAscensionRate

        #region Declination
        [TestMethod]
        public void Declination_ReturnsRAFromAxisController()
        {
            telescope.DecAxisController.Position = 12.23;

            Assert.AreEqual(telescope.DecAxisController.Position, telescope.Declination);
        }
        #endregion Declination

        #region DeclinationRate
        [TestMethod]
        public void GetDeclinationRate_Returns0()
        {
            Assert.AreEqual(0, telescope.Declination);
        }

        [TestMethod]
        [ExpectedException(typeof(PropertyNotImplementedException))]
        public void SetDeclinationRate_ThrowsNotImplemented()
        {
            telescope.DeclinationRate = 10;
        }
        #endregion DeclinationRate

        #region CanSetRightAscensionRate
        [TestMethod]
        public void CanSetDeclinationRate_ReturnsFalse()
        {
            Assert.IsFalse(telescope.CanSetDeclinationRate);
        }
        #endregion CanSetDeclinationRate

        #region MoveAxis
        [TestMethod]
        [ExpectedException(typeof(InvalidValueException))]
        public void MoveAxis_TertiaryAxis_ThrowsException()
        {
            telescope.MoveAxis(TelescopeAxes.axisTertiary, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidValueException))]
        public void MoveAxis_PrimaryAxisRateOverMaxPositive_ThrowsException()
        {
            telescope.MoveAxis(TelescopeAxes.axisPrimary, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidValueException))]
        public void MoveAxis_PrimaryAxisRateOverMaxNegative_ThrowsException()
        {
            telescope.MoveAxis(TelescopeAxes.axisPrimary, -10);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidValueException))]
        public void MoveAxis_SecondaryAxisRateOverMaxPositive_ThrowsException()
        {
            telescope.MoveAxis(TelescopeAxes.axisSecondary, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidValueException))]
        public void MoveAxis_SecondaryAxisRateOverMaxNegative_ThrowsException()
        {
            telescope.MoveAxis(TelescopeAxes.axisSecondary, -10);
        }

        [TestMethod]
        public void MoveAxis_PrimaryAxisStartStopSlew_StartsStopsSlewing()
        {
            telescope.RAAxisController.Connect();
            telescope.MoveAxis(TelescopeAxes.axisPrimary, telescope.AxisRates(TelescopeAxes.axisPrimary)[1].Maximum);

            Assert.IsTrue(telescope.RAAxisController.Slewing);
            Assert.IsFalse(telescope.DecAxisController.Slewing);
            Assert.IsTrue(telescope.Slewing);

            telescope.MoveAxis(TelescopeAxes.axisPrimary, 0);

            Assert.IsFalse(telescope.RAAxisController.Slewing);
            Assert.IsFalse(telescope.DecAxisController.Slewing);
            Assert.IsFalse(telescope.Slewing);
        }

        [TestMethod]
        public void MoveAxis_SecondaryAxisStartStopSlew_StartsStopsSlewing()
        {
            telescope.DecAxisController.Connect();
            telescope.MoveAxis(TelescopeAxes.axisSecondary, telescope.AxisRates(TelescopeAxes.axisSecondary)[1].Maximum);

            Assert.IsTrue(telescope.DecAxisController.Slewing);
            Assert.IsFalse(telescope.RAAxisController.Slewing);
            Assert.IsTrue(telescope.Slewing);

            telescope.MoveAxis(TelescopeAxes.axisSecondary, 0);

            Assert.IsFalse(telescope.DecAxisController.Slewing);
            Assert.IsFalse(telescope.RAAxisController.Slewing);
            Assert.IsFalse(telescope.Slewing);
        }

        [TestMethod]
        public void MoveAxis_DualAxisStartStopSlew_StartsStopsSlewing()
        {
            telescope.RAAxisController.Connect();
            telescope.DecAxisController.Connect();
            telescope.MoveAxis(TelescopeAxes.axisPrimary, telescope.AxisRates(TelescopeAxes.axisPrimary)[1].Maximum);
            telescope.MoveAxis(TelescopeAxes.axisSecondary, telescope.AxisRates(TelescopeAxes.axisSecondary)[1].Maximum);

            Assert.IsTrue(telescope.RAAxisController.Slewing);
            Assert.IsTrue(telescope.DecAxisController.Slewing);
            Assert.IsTrue(telescope.Slewing);

            telescope.MoveAxis(TelescopeAxes.axisPrimary, 0);

            Assert.IsFalse(telescope.RAAxisController.Slewing);
            Assert.IsTrue(telescope.DecAxisController.Slewing);          
            Assert.IsTrue(telescope.Slewing);

            telescope.MoveAxis(TelescopeAxes.axisSecondary, 0);

            Assert.IsFalse(telescope.RAAxisController.Slewing);
            Assert.IsFalse(telescope.DecAxisController.Slewing);
            Assert.IsFalse(telescope.Slewing);
        }
        #endregion MoveAxis

        #region Slewing
        [TestMethod]
        public void Slewing_FalseByDefault()
        {
            Assert.IsFalse(telescope.Slewing);
        }

        [TestMethod]
        public void Slewing_TrueIfRAAxisSlewing()
        {
            telescope.RAAxisController.Connect();
            telescope.RAAxisController.StartSlew(Orientation.Plus);
            Assert.IsTrue(telescope.Slewing);
        }

        [TestMethod]
        public void Slewing_TrueIfDecAxisSlewing()
        {
            telescope.DecAxisController.Connect();
            telescope.DecAxisController.StartSlew(Orientation.Plus);
            Assert.IsTrue(telescope.Slewing);
        }

        [TestMethod]
        public void Slewing_TrueIfDecAxisAndRAAxisSlewing()
        {
            telescope.RAAxisController.Connect();
            telescope.DecAxisController.Connect();
            telescope.DecAxisController.StartSlew(Orientation.Plus);
            telescope.RAAxisController.StartSlew(Orientation.Plus);
            Assert.IsTrue(telescope.Slewing);
        }
        #endregion Slewing
    }
}
