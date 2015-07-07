using System;
using MeetingHelper.Helpers.TimeKeerpers;

using NUnit.Framework;
using System.Threading;

namespace MeetingHelper.Tests.Helpers
{
    [TestFixture]
    public class TimeHelperTests
    {
        [TestCase(TimeHelper.TimerStatus.STOPPED, TimeHelper.TimerStatus.RUNNING)]
        [TestCase(TimeHelper.TimerStatus.RUNNING, TimeHelper.TimerStatus.PAUSED)]
        [TestCase(TimeHelper.TimerStatus.PAUSED, TimeHelper.TimerStatus.RUNNING)]
        public void TimerClicked_TimerStatusChangedCorrectly(TimeHelper.TimerStatus before, TimeHelper.TimerStatus expectedAfter)
        {
            TestableTimeHelper timeHelper = new TestableTimeHelper();
            timeHelper.SetCurrentStatus(before);

            timeHelper.TimerClicked();

            Assert.AreEqual(timeHelper.CurrentStatus, expectedAfter);
        }

        [TestCase(TimeHelper.TimerStatus.STOPPED)]
        [TestCase(TimeHelper.TimerStatus.RUNNING)]
        [TestCase(TimeHelper.TimerStatus.PAUSED)]
        public void Reset_TimerStatusChangedToStopped(TimeHelper.TimerStatus before)
        {
            TestableTimeHelper timeHelper = new TestableTimeHelper();
            timeHelper.SetCurrentStatus(before);

            timeHelper.Reset();

            Assert.AreEqual(timeHelper.CurrentStatus, TimeHelper.TimerStatus.STOPPED);
        }

        [TestCase(TimeHelper.TimerStatus.STOPPED, true)]
        [TestCase(TimeHelper.TimerStatus.PAUSED, true)]
        [TestCase(TimeHelper.TimerStatus.RUNNING, false)]
        public void TimerClicked_TimerRunningOrPausing(TimeHelper.TimerStatus status, bool timerRunningAfterClick)
        {
            TestableTimeHelper timeHelper = new TestableTimeHelper();
            timeHelper.SetCurrentStatus(status);

            timeHelper.TimerClicked();
            TimeSpan earlier = timeHelper.GetCurrentTime();
            Thread.Sleep(10);
            TimeSpan later = timeHelper.GetCurrentTime();

            Assert.AreEqual(later > earlier, timerRunningAfterClick);
        }
    }


    internal class TestableTimeHelper : TimeHelper
    {
        public void SetCurrentStatus(TimeHelper.TimerStatus status)
        {
            base.CurrentStatus = status;
        }

        public TimeSpan GetCurrentTime()
        {
            return new TimeSpan();
        }
    }
}
