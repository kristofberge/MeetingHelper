using System;
using System.Threading;

using MeetingHelper.Helpers.Time;

using NUnit.Framework;

using MeetingHelper.Tests.Testables;
using MeetingHelper.Shared;
using Moq;
using System.Windows.Threading;
using Moq.Protected;

namespace MeetingHelper.Tests.Helpers.Time
{
    [TestFixture]
    public class TimeHelperTests
    {
        private Mock<TestableTimeHelper> _timeHelper;

        [SetUp]
        public void Setup()
        {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _timeHelper = new Mock<TestableTimeHelper>(timer);
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

        [TestCase(Common.TimerStatus.STOPPED)]
        [TestCase(Common.TimerStatus.PAUSED)]
        public void TimerRunning_UpdateDisplayTimeCalled(Common.TimerStatus statusBeforeClick)
        {
            //Arrange
            _timeHelper.Object.SetCurrentStatus(statusBeforeClick);
            _timeHelper.Protected().Setup("UpdateTimeToDisplay", ItExpr.IsAny<object>(), ItExpr.IsAny<EventArgs>()).Verifiable();
            _timeHelper.CallBase = true;

            //Act
            _timeHelper.Object.TimerClicked();
            Thread.Sleep(2000);

            //Assert
            _timeHelper.Protected().Verify("UpdateTimeToDisplay", Times.AtLeastOnce(), ItExpr.IsAny<object>(), ItExpr.IsAny<EventArgs>());
        }
    }
}
