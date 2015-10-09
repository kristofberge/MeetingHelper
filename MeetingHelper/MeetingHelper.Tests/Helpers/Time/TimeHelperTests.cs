using System;

using MeetingHelper.Helpers.Time;

using NUnit.Framework;

using MeetingHelper.Tests.Testables;
using MeetingHelper.Shared;
using Moq;
using Moq.Protected;
using System.Windows.Threading;
using System.Threading;

namespace MeetingHelper.Tests.Helpers.Time
{
    [TestFixture]
    public class TimeHelperTests
    {
        private Mock<TestableTimeHelper> _timeHelper;
        private DispatcherTimer _timer;

        [SetUp]
        public void Setup()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _timeHelper = new Mock<TestableTimeHelper>(_timer);
        }

        [TestCase(Common.TimerStatus.STOPPED, Common.TimerStatus.RUNNING)]
        [TestCase(Common.TimerStatus.RUNNING, Common.TimerStatus.PAUSED)]
        [TestCase(Common.TimerStatus.PAUSED, Common.TimerStatus.RUNNING)]
        public void TimerClicked_TimerStatusChangedCorrectly(Common.TimerStatus before, Common.TimerStatus expectedAfter)
        {
            //Arrange
            _timeHelper.Object.SetCurrentStatus(before);

            //Act
            _timeHelper.Object.TimerClicked();

            //Assert
            Assert.AreEqual(_timeHelper.Object.CurrentStatus, expectedAfter);
        }

        [TestCase(Common.TimerStatus.STOPPED)]
        [TestCase(Common.TimerStatus.RUNNING)]
        [TestCase(Common.TimerStatus.PAUSED)]
        public void Reset_TimerStatusChangedToStopped(Common.TimerStatus before)
        {
            //Arrange
            _timeHelper.Object.SetCurrentStatus(before);

            //Act
            _timeHelper.Object.Reset();

            //Asser
            Assert.AreEqual(_timeHelper.Object.CurrentStatus, Common.TimerStatus.STOPPED);
        }

        [TestCase(Common.TimerStatus.STOPPED, true)]
        [TestCase(Common.TimerStatus.PAUSED, true)]
        [TestCase(Common.TimerStatus.RUNNING, false)]
        public void TimerClicked_TimerRunningOrPaused(Common.TimerStatus statusBeforeClick, bool timerRunningAfterClick)
        {
            //Arrange
            _timeHelper.Object.SetCurrentStatus(statusBeforeClick);

            //Act
            _timeHelper.Object.TimerClicked();

            //Assert
            Assert.AreEqual(_timeHelper.Object.IsTimerEnabled(), timerRunningAfterClick);
        }

        [Test]
        public void UpdateTimeToDisplay_UpdatesTimeToDisplay()
        {
            //Arrange
            _timeHelper.Protected().SetupSet<TimeSpan>("TimeToDisplay", ItExpr.IsAny<TimeSpan>()).Verifiable();

            //Act
            _timeHelper.Object.CallUpdateTimeToDisplay();

            //Assert
            _timeHelper.Protected().VerifySet<TimeSpan>("TimeToDisplay", Times.Once(), ItExpr.IsAny<TimeSpan>());
        }

        [Test]
        public void UpdateTimeToDisplay_CallsOnTimeUpdated()
        {
            //Arrange
            var onTimeUpdatedHasRun = false;
            _timeHelper.Protected().Setup("OnTimeUpdated").Callback(() => { onTimeUpdatedHasRun = true; });

            //Act
            _timeHelper.Object.CallUpdateTimeToDisplay();

            //Assert
            Assert.IsTrue(onTimeUpdatedHasRun);
        }

        [Ignore] //ignore for now, until I've implemented a solution for the Dispatcher problem.
        [TestCase(Common.TimerStatus.STOPPED)]
        [TestCase(Common.TimerStatus.PAUSED)]
        public void TimerRunning_UpdateDisplayTimeCalled(Common.TimerStatus statusBeforeClick)
        {
            //Arrange
            var timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            
            var timeHelper = new Mock<TimeHelper>(_timer) { CallBase = true };
            timeHelper.Protected().Setup("UpdateTimeToDisplay", ItExpr.IsAny<object>(), ItExpr.IsAny<EventArgs>()).Verifiable();

            //Act
            timeHelper.Object.TimerClicked();
            Thread.Sleep(20);

            //Assert
            timeHelper.Protected().Verify("UpdateTimeToDisplay", Times.AtLeastOnce(), ItExpr.IsAny<object>(), ItExpr.IsAny<EventArgs>());
        }
    }
}
