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

        [TestCase(Constants.TimerStatus.STOPPED, Constants.TimerStatus.RUNNING)]
        [TestCase(Constants.TimerStatus.RUNNING, Constants.TimerStatus.PAUSED)]
        [TestCase(Constants.TimerStatus.PAUSED, Constants.TimerStatus.RUNNING)]
        public void TimerClicked_TimerStatusChangedCorrectly(Constants.TimerStatus before, Constants.TimerStatus expectedAfter)
        {
            //Arrange
            _timeHelper.Object.SetCurrentStatus(before);

            //Act
            _timeHelper.Object.TimerClicked();

            //Assert
            Assert.AreEqual(_timeHelper.Object.CurrentStatus, expectedAfter);
        }

        [TestCase(Constants.TimerStatus.STOPPED)]
        [TestCase(Constants.TimerStatus.RUNNING)]
        [TestCase(Constants.TimerStatus.PAUSED)]
        public void Reset_TimerStatusChangedToStopped(Constants.TimerStatus before)
        {
            //Arrange
            _timeHelper.Object.SetCurrentStatus(before);

            //Act
            _timeHelper.Object.Reset();

            //Asser
            Assert.AreEqual(_timeHelper.Object.CurrentStatus, Constants.TimerStatus.STOPPED);
        }

        [TestCase(Constants.TimerStatus.STOPPED, true)]
        [TestCase(Constants.TimerStatus.PAUSED, true)]
        [TestCase(Constants.TimerStatus.RUNNING, false)]
        public void TimerClicked_TimerRunningOrPaused(Constants.TimerStatus statusBeforeClick, bool timerRunningAfterClick)
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
            _timeHelper.CallBase = true;
            _timeHelper.Protected().Setup("OnTimeUpdated").Verifiable();

            //Act
            _timeHelper.Object.CallUpdateTimeToDisplay();

            //Assert
            _timeHelper.Protected().Verify("OnTimeUpdated", Times.AtLeastOnce());
        }

        [Ignore] //Ignore for now, until I've implemented a solution for the Dispatcher problem: as this does not run in a WPF environment, the Dispatcher will not automatically start.
        [TestCase(Constants.TimerStatus.STOPPED)]
        [TestCase(Constants.TimerStatus.PAUSED)]
        public void TimerRunning_UpdateDisplayTimeCalled(Constants.TimerStatus statusBeforeClick)
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

        [Test]
        public void OnTimeUpdated_RaisedTimeUpdatedEvent()
        {
            //Arrange
            bool eventLaunched = false;
            _timeHelper.Raise(x => x.OnTimeUpdated += null, TimeUpdatedEventArgs.Empty);

            //Act
            _timeHelper.Object.CallOnTimeUpdated();

            //Assert
            _timeHelper.VerifyAll();
        }
    }
    }
}
