using System;
using MeetingHelper.Helpers.Time;

using NUnit.Framework;
using System.Threading;
using MeetingHelper.Tests.Testables;

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
    }
}
