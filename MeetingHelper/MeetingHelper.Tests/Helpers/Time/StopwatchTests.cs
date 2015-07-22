using System;
using NUnit.Framework;
using System.Threading;
using Moq;
using MeetingHelper.Helpers.Time;
using MeetingHelper.Tests.Testables;

namespace MeetingHelper.Tests.Helpers.Time
{
    [TestFixture]
    public class StopwatchTests
    {
        
        [TestCase(TimeHelper.TimerStatus.STOPPED, true)]
        [TestCase(TimeHelper.TimerStatus.PAUSED, true)]
        [TestCase(TimeHelper.TimerStatus.RUNNING, false)]
        public void TimerClicked_TimerRunningOrPausing(TimeHelper.TimerStatus status, bool timerRunningAfterClick)
        {
            var timeHelper = new TestableStopwatch();
            timeHelper.SetCurrentStatus(status);

            timeHelper.TimerClicked();
            TimeSpan earlier = timeHelper.GetCurrentTime();
            Thread.Sleep(10);
            TimeSpan later = timeHelper.GetCurrentTime();

            Assert.AreEqual(later > earlier, timerRunningAfterClick);
        }
    }
}
