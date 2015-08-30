using System;
using System.Threading;
using ASCOM.Norgate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Norgate.ASCOM.Driver.UnitTests
{
    [TestClass]
    public class DecAxisControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(AxisNotStartedException))]
        public void Slew_ThrowsIfNotStarted()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);

            controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));
        }


        [TestMethod]
        public void Slew_Plus_StartsTrackingPosition()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();

            var task = controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(Math.Round((Constants.DEGREES_PER_SECOND * 8) * 10, 4), Math.Round(controller.Position, 4));
        }

        [TestMethod]
        public void Slew_Plus_PositionUpdatesAtSlewRatePoitiveStart()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = 50;

            var task = controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(Math.Round(50 + (Constants.DEGREES_PER_SECOND * 8) * 10, 4), Math.Round(controller.Position, 4));
        }

        [TestMethod]
        public void Slew_Plus_PositionUpdatesAtSlewRateNegativeStart()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = -50;

            var task = controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(Math.Round(-50 + (Constants.DEGREES_PER_SECOND * 8) * 10, 4), Math.Round(controller.Position, 4));
        }

        [TestMethod]
        public void Slew_Plus_ReturnsToNotMovingAfterSlew()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = TimeSpan.FromSeconds(180).TotalHours;

            var task = controller.Slew(Orientation.Plus, TimeSpan.FromSeconds(10));
            Assert.AreEqual(8, controller.SlewRate);
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(0, controller.SlewRate);
        }

        [TestMethod]
        public void Slew_Plus_StopsWhenReachs90()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = 80;

            var slewTime = TimeSpan.FromSeconds(20/(Constants.DEGREES_PER_SECOND*8));
            var task = controller.Slew(Orientation.Plus, slewTime);

            //Simulate time ticking by
            slewTime = TimeSpan.FromSeconds(5 / (Constants.DEGREES_PER_SECOND * 8));
            fakeTelescope.SiderealTime += slewTime.TotalHours;
            Thread.Sleep(20);
            fakeTelescope.SiderealTime += slewTime.TotalHours;
            Thread.Sleep(20);
            fakeTelescope.SiderealTime += slewTime.TotalHours;
            Thread.Sleep(20);
            fakeTelescope.SiderealTime += slewTime.TotalHours;
            Thread.Sleep(20);

            task.Wait();
            Assert.AreEqual(90, controller.Position);
        }

        [TestMethod]
        public void Slew_Minus_StartsTrackingPosition()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();

            var task = controller.Slew(Orientation.Minus, TimeSpan.FromSeconds(10));

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(-(Math.Round((Constants.DEGREES_PER_SECOND * 8) * 10, 4)), Math.Round(controller.Position, 4));
        }

        [TestMethod]
        public void Slew_Minus_PositionUpdatesAtSlewRatePositiveStart()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = 50;

            var task = controller.Slew(Orientation.Minus, TimeSpan.FromSeconds(10));

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(Math.Round(50 - (Constants.DEGREES_PER_SECOND * 8) * 10, 4), Math.Round(controller.Position, 4));
        }

        [TestMethod]
        public void Slew_Minus_PositionUpdatesAtSlewRateNegativeStart()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = -50;

            var task = controller.Slew(Orientation.Minus, TimeSpan.FromSeconds(10));

            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(Math.Round(-50 - (Constants.DEGREES_PER_SECOND * 8) * 10, 4), Math.Round(controller.Position, 4));
        }

        [TestMethod]
        public void Slew_Minus_ReturnsToNotMovingAfterSlew()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = TimeSpan.FromSeconds(180).TotalHours;

            var task = controller.Slew(Orientation.Minus, TimeSpan.FromSeconds(10));
            Assert.AreEqual(-8, controller.SlewRate);
            fakeTelescope.SiderealTime += TimeSpan.FromSeconds(10).TotalHours;

            task.Wait();
            Assert.AreEqual(0, controller.SlewRate);
        }

        [TestMethod]
        public void Slew_Minus_StopsWhenReachsMinus90()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();
            controller.Position = -80;

            var slewTime = TimeSpan.FromSeconds(20 / (Constants.DEGREES_PER_SECOND * 8));
            var task = controller.Slew(Orientation.Minus, slewTime);

            //Simulate time ticking by
            slewTime = TimeSpan.FromSeconds(5 / (Constants.DEGREES_PER_SECOND * 8));
            fakeTelescope.SiderealTime += slewTime.TotalHours;
            Thread.Sleep(20);
            fakeTelescope.SiderealTime += slewTime.TotalHours;
            Thread.Sleep(20);
            fakeTelescope.SiderealTime += slewTime.TotalHours;
            Thread.Sleep(20);
            fakeTelescope.SiderealTime += slewTime.TotalHours;
            Thread.Sleep(20);

            task.Wait();
            Assert.AreEqual(-90, controller.Position);
        }

        [TestMethod]
        public void StartStopSlew_Plus_SetsSlewRate()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();

            controller.StartSlew(Orientation.Plus);

            Assert.AreEqual(8, controller.SlewRate);

            controller.StopSlew();

            Assert.AreEqual(0, controller.SlewRate);
        }

        [TestMethod]
        public void StartStopSlew_Minus_SetsSlewRate()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);
            controller.Connect();

            controller.StartSlew(Orientation.Minus);

            Assert.AreEqual(-8, controller.SlewRate);

            controller.StopSlew();

            Assert.AreEqual(0, controller.SlewRate);
        }

        [TestMethod]
        [ExpectedException(typeof(AxisNotStartedException))]
        public void StartSlew_ThrowsIfNotStarted()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);

            controller.StartSlew(Orientation.Plus);
        }

        [TestMethod]
        [ExpectedException(typeof(AxisNotStartedException))]
        public void StopSlew_ThrowsIfNotStarted()
        {
            var fakeTelescope = new FakeTelescope();
            fakeTelescope.SiderealTime = TimeSpan.FromHours(3).TotalHours;
            var controller = new DecAxisController(fakeTelescope);

            controller.StopSlew();
        }
    }
}
