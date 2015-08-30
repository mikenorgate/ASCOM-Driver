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
        [ExpectedException(typeof(AxisNotStartedException))]
        public void Slew_ThrowsIfNotStarted()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);

            controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));
        }

        [TestMethod]
        public void Start_StartsTrackingPosition()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(10, Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void Slew_Plus_PositionUpdatesAtSlewRate()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = TimeSpan.FromSeconds(180).TotalHours;

            var task = controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(260, Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void Slew_Plus_SetsSlewing()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();

            var task = controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));
            Assert.IsTrue(controller.Slewing);

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.IsFalse(controller.Slewing);
        }

        [TestMethod]
        public void Slew_Plus_ReturnsToTrackingRateAfterSlew()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = TimeSpan.FromSeconds(180).TotalHours;

            var task = controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));
            Assert.AreEqual(8, controller.SlewRate);
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(1, controller.SlewRate);
        }

        [TestMethod]
        public void Slew_Plus_ReturnsTo0WhenPassedEndOfDay()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = Constants.HOURS_PER_SIDEREAL_DAY;

            var task = controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(80, Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void Slew_Minus_SetsSlewing()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();

            var task = controller.Slew(Orientation.Minus, TimeSpan.FromSeconds(10));
            Assert.IsTrue(controller.Slewing);

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.IsFalse(controller.Slewing);
        }

        [TestMethod]
        public void Slew_Minus_PositionUpdatesAtSlewRate()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = TimeSpan.FromSeconds(180).TotalHours; ;

            var task = controller.Slew(Orientation.Minus, TimeSpan.FromSeconds(10));

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(100, Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void Slew_Minus_ReturnsToTrackingRateAfterSlew()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = TimeSpan.FromSeconds(180).TotalHours;

            var task = controller.Slew(Orientation.Minus, TimeSpan.FromSeconds(10));
            Assert.AreEqual(-8, controller.SlewRate);
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(1, controller.SlewRate);
        }

        [TestMethod]
        public void Slew_Plus_ReturnsToEndOfDatWhenPassed0()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = 0;

            var task = controller.Slew(Orientation.Minus, TimeSpan.FromSeconds(10));
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(Math.Round(TimeSpan.FromHours(Constants.HOURS_PER_SIDEREAL_DAY - (TimeSpan.FromSeconds(80).TotalHours)).TotalSeconds), Math.Round(location.TotalSeconds));
        }

        [TestMethod]
        public void GetPosition_SiderealTimeBecomesLessThanStartTime()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = 12d;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();

            fakeTelescope.SiderealTime = 11d;
            var location = TimeSpan.FromHours(controller.Position);
            Assert.AreEqual(Math.Round(Constants.HOURS_PER_SIDEREAL_DAY - 1, 6), Math.Round(location.TotalHours, 6));
        }

        [TestMethod]
        public void StartStopSlew_Plus_SetsSlewRate()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();

            controller.StartSlew(Orientation.Plus);
            
            Assert.AreEqual(8, controller.SlewRate);

            controller.StopSlew();

            Assert.AreEqual(1, controller.SlewRate);
        }

        [TestMethod]
        public void StartStopSlew_Minus_SetsSlewRate()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();

            controller.StartSlew(Orientation.Minus);

            Assert.AreEqual(-8, controller.SlewRate);

            controller.StopSlew();

            Assert.AreEqual(1, controller.SlewRate);
        }

        [TestMethod]
        public void StartStopSlew_Minus_SetsSlewing()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);
            controller.Connect();

            controller.StartSlew(Orientation.Minus);

            Assert.IsTrue(controller.Slewing);

            controller.StopSlew();

            Assert.IsFalse(controller.Slewing);
        }

        [TestMethod]
        [ExpectedException(typeof(AxisNotStartedException))]
        public void StartSlew_ThrowsIfNotStarted()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);

            controller.StartSlew(Orientation.Plus);
        }

        [TestMethod]
        [ExpectedException(typeof(AxisNotStartedException))]
        public void StopSlew_ThrowsIfNotStarted()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new RAAxisController(fakeTelescope);

            controller.StopSlew();
        }
    }
}
