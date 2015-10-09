using System;
using NUnit.Framework;
using System.Threading;
using Moq;
using MeetingHelper.Helpers.Time;
using MeetingHelper.Tests.Testables;
using System.Windows.Threading;

namespace MeetingHelper.Tests.Helpers.Time
{
    [TestFixture]
    public class StopwatchTests
    {
        #region Setup
        private TestableStopwatch _stopwatch;

        [SetUp]
        public void Setup()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _stopwatch = new TestableStopwatch(timer);
        }
        #endregion

        [Test]
        public void CalculateTimeToBeDisplayed_CorrectTimeWithoutTimeBeforePaused()
        {

        }
    }
}