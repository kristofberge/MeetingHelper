using MeetingHelper.Helpers.Time;
using MeetingHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Tests.Testables
{
    public class TestableStopwatch : Stopwatch
    {
        public TimeSpan GetCurrentTime()
        {
            return base.CalculateTimeToBeDisplayed();
        }

        public void SetCurrentStatus(Common.TimerStatus status)
        {
            base.CurrentStatus = status;
        }
    }
}
