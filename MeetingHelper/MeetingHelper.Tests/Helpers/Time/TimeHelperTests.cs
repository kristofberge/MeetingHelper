using System;

using MeetingHelper.Helpers.Time;

using NUnit.Framework;

using MeetingHelper.Tests.Testables;
using MeetingHelper.Shared;
using Moq;
using Moq.Protected;
using System.Windows.Threading;
using System.Threading;
using MeetingHelper.Events;

namespace MeetingHelper.Tests.Helpers.Time
{
    [TestFixture]
    public class TimeHelperTests
    {
        #region Setup
        private Mock<TestableTimeHelper> _timeHelper;
        private DispatcherTimer _timer;
        private Mock<IShared> _shared;

        [SetUp]
        public void Setup()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            _timeHelper = new Mock<TestableTimeHelper>(_timer) { CallBase = true };

            _shared = new Mock<IShared>();
            _shared.SetupGet(x => x.CurrentTime).Returns(new DateTimeOffset());
            _timeHelper.Protected().SetupGet<IShared>("Shared").Returns(_shared.Object);
        }
        #endregion

        #region Status
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
        #endregion

        #region Time Update
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
            _timeHelper.Protected().Setup("OnTimeUpdated").Verifiable();

            //Act
            _timeHelper.Object.CallUpdateTimeToDisplay();

            //Assert
            _timeHelper.Protected().Verify("OnTimeUpdated", Times.AtLeastOnce());
        }

        [Test]
        public void OnTimeUpdated_RaisedTimeUpdatedEvent()
        {
            //Arrange
            var eventRaised = false;
            _timeHelper.Object.TimeUpdated += delegate (object sender, TimeUpdatedEventArgs e)
            {
                eventRaised = true;
            };

            //Act
            _timeHelper.Object.CallOnTimeUpdated();

            //Assert
            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void OnTimeUpdated_TimeToDisplayPassedInEvent()
        {
            //Arrange
            TimeUpdatedEventArgs receivedEvent = null;
            var timeToDisplay = new TimeSpan(1, 24, 17);
            _timeHelper.Protected().SetupGet<TimeSpan>("TimeToDisplay").Returns(timeToDisplay);
            _timeHelper.Object.TimeUpdated += delegate (object sender, TimeUpdatedEventArgs e)
            {
                receivedEvent = e;
            };

            //Act
            _timeHelper.Object.CallOnTimeUpdated();

            //Assert
            Assert.AreEqual(receivedEvent.Time, timeToDisplay);
        }
        #endregion

        #region Time keeping
        [Test]
        public void TimerStarted_CurentTimeSetAsTimeStarted()
        {
            //Arrange
            var expected = DateTimeOffset.Now;
            _shared.SetupGet(x => x.CurrentTime).Returns(expected);
            _timeHelper.Object.SetCurrentStatus(Constants.TimerStatus.STOPPED);

            //Act
            _timeHelper.Object.TimerClicked();

            //Assert
            Assert.AreEqual(expected, _timeHelper.Object.GetTimeStarted());
        }

        [TestCase(0, 2, 30, 0)]
        [TestCase(3, 10, 30, 0)]
        [TestCase(0, 30, 10, 90)]
        [TestCase(1, 20, 0, 60)]
        [TestCase(30, 59, 59, 99)]
        public void TimerPaused_SetTimeRunningBeforePaused(int hrs, int mins, int secs, int ms)
        {
            //Arrange
            var timeStarted = DateTimeOffset.Now;
            var expected = new TimeSpan(0, hrs, mins, secs, ms);
            _timeHelper.Protected().SetupGet<DateTimeOffset>("TimeStarted").Returns(timeStarted);

            var timeOnPause = timeStarted + expected;
            _shared.SetupGet(x => x.CurrentTime).Returns(timeOnPause);

            _timeHelper.Object.SetCurrentStatus(Constants.TimerStatus.RUNNING);

            //Act
            _timeHelper.Object.TimerClicked();

            //Assert
            Assert.AreEqual(_timeHelper.Object.GetTimeRunningBeforePause(), expected);
        }
        #endregion

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
    }
}
