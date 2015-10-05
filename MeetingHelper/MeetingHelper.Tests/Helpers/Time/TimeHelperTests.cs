using System;
using System.Threading;

using MeetingHelper.Helpers.Time;

using NUnit.Framework;

using MeetingHelper.Tests.Testables;
using MeetingHelper.Shared;

namespace MeetingHelper.Tests.Helpers.Time
{
    [TestFixture]
    public class TimeHelperTests
    {
        [TestCase(Common.TimerStatus.STOPPED, Common.TimerStatus.RUNNING)]
        [TestCase(Common.TimerStatus.RUNNING, Common.TimerStatus.PAUSED)]
        [TestCase(Common.TimerStatus.PAUSED, Common.TimerStatus.RUNNING)]
        public void TimerClicked_TimerStatusChangedCorrectly(Common.TimerStatus before, Common.TimerStatus expectedAfter)
        {
            TestableTimeHelper timeHelper = new TestableTimeHelper();
            timeHelper.SetCurrentStatus(before);

            timeHelper.TimerClicked();

            Assert.AreEqual(timeHelper.CurrentStatus, expectedAfter);
        }

        [TestCase(Common.TimerStatus.STOPPED)]
        [TestCase(Common.TimerStatus.RUNNING)]
        [TestCase(Common.TimerStatus.PAUSED)]
        public void Reset_TimerStatusChangedToStopped(Common.TimerStatus before)
        {
            TestableTimeHelper timeHelper = new TestableTimeHelper();
            timeHelper.SetCurrentStatus(before);

            timeHelper.Reset();

            Assert.AreEqual(timeHelper.CurrentStatus, Common.TimerStatus.STOPPED);
        }

        [TestCase(Common.TimerStatus.STOPPED, true)]
        [TestCase(Common.TimerStatus.PAUSED, true)]
        [TestCase(Common.TimerStatus.RUNNING, false)]
        public void TimerClicked_TimerRunningOrPaused(Common.TimerStatus status, bool timerRunningAfterClick)
        {
            var timeHelper = new TestableTimeHelper();
            timeHelper.SetCurrentStatus(status);

            timeHelper.TimerClicked();

            Assert.AreEqual(timeHelper.CallIsTimerEnabled, timerRunningAfterClick);
        }
    }
}
