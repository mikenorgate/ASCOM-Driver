using System;
using System.Threading;
using ASCOM.Norgate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Norgate.ASCOM.Driver.UnitTests
{
    [TestClass]
    public class RAAxisControllerTests
    {
        [TestMethod]
        public void Ctor_StartsTrackingPosition()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(10, Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void SetSlewRate_PositionUpdatesAtSlewRate()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);

            controller.SlewRate = 1;
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(10, Math.Round(location.TotalSeconds));

            controller.SlewRate = 8;
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(90, Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void SetSlewRate_NegativeSlew_PositionUpdatesAtSlewRate()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);

            controller.SlewRate = -1;
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(Math.Round(Constants.SECONDS_PER_SIDEREAL_DAY - 10), Math.Round(location.TotalSeconds));

            controller.SlewRate = -8;
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(Math.Round(Constants.SECONDS_PER_SIDEREAL_DAY - 90), Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void SetPosition_PositionUpdatesToNewPositionAndContinuesTracking()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);

            controller.Position = TimeSpan.FromHours(1).TotalHours;
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(3610, Math.Round(location.TotalSeconds));

            controller.SlewRate = 8;
            controller.Position = TimeSpan.FromHours(13).TotalHours;
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(46880, Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void SetPosition_NegativeSlew_PositionUpdatesToNewPositionAndContinuesTracking()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);

            controller.Position = TimeSpan.FromHours(1).TotalHours;
            controller.SlewRate = -1;
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(3590, Math.Round(location.TotalSeconds));

            controller.Position = TimeSpan.FromHours(13).TotalHours;
            controller.SlewRate = -8;            
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(46720, Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void GetPosition_SiderealTimeBecomesLessThanStartTime()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = 12d;
            var controller = new RAAxisController(fakeTelescope);

            fakeTelescope.SiderealTime = 11d;
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(Math.Round(Constants.HOURS_PER_SIDEREAL_DAY - 1, 6), Math.Round(location.TotalHours, 6));
        }
    }
}
