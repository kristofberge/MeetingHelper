using System;
using NUnit.Framework;
using System.Threading;
using Moq;
using Moq.Protected;
using MeetingHelper.Helpers.Time;
using MeetingHelper.Tests.Testables;
using System.Windows.Threading;
using MeetingHelper.Shared;
using System.Collections.Generic;

namespace MeetingHelper.Tests.Helpers.Time
{
    [TestFixture]
    public class StopwatchTests
    {
        #region Setup
        private Mock<TestableStopwatch> _stopwatch;
        private DispatcherTimer _timer;
        private Mock<IShared> _shared;

        [SetUp]
        public void Setup()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _stopwatch = new Mock<TestableStopwatch>(_timer) { CallBase = true };
            _shared = new Mock<IShared>();
            _stopwatch.Protected().SetupGet<IShared>("Shared").Returns(_shared.Object);
        }
        #endregion

        #region CalculateTimeToBeDisplayed
        [TestCase(0, 2, 30, 0)]
        [TestCase(3, 10, 30, 0)]
        [TestCase(0, 30, 10, 90)]
        [TestCase(1, 20, 0, 60)]
        [TestCase(30, 59, 59, 99)]
        public void CalculateTimeToBeDisplayed_CorrectTimeNoTimeBeforePaused(int hrs, int mins, int secs, int msecs)
        {
            //Arrange
            var expected = new TimeSpan(0, hrs, mins, secs, msecs);
            DateTimeOffset currentTime = DateTimeOffset.Now;
            _shared.SetupGet(x => x.CurrentTime).Returns(currentTime);

            DateTimeOffset timeStarted = currentTime - expected;
            _stopwatch.Protected().SetupGet<DateTimeOffset>("TimeStarted").Returns(timeStarted);

            //Act
            var result = _stopwatch.Object.CallCalculateTimeToBeDisplayed();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(0, 2, 30, 0)]
        [TestCase(3, 10, 30, 0)]
        [TestCase(0, 30, 10, 90)]
        [TestCase(1, 20, 0, 60)]
        [TestCase(30, 59, 59, 99)]
        public void CalculateTimeToBeDisplayed_CorrectTimeWithTimeBeforePaused(int hrs, int mins, int secs, int msecs)
        {
            //Arrange
            var expected = new TimeSpan(0, hrs, mins, secs, msecs);
            var timeRunningBeforePause = new TimeSpan(0, 1, 25, 95); // 00:01:25.95
            var currentTime = DateTimeOffset.Now;
            var timeStarted = currentTime - timeRunningBeforePause - expected;

            _shared.SetupGet(x => x.CurrentTime).Returns(currentTime);

            _stopwatch.Protected().SetupGet<DateTimeOffset>("TimeStarted").Returns(timeStarted);
            _stopwatch.Protected().SetupGet<TimeSpan>("TimeRunningBeforePause").Returns(timeRunningBeforePause);

            //Act
            var result = _stopwatch.Object.CallCalculateTimeToBeDisplayed();

            //Assert
            Assert.AreEqual(expected, result);
        }
        #endregion
    }
}