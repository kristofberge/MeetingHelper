using MeetingHelper.Helpers.Time;
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

        public void SetCurrentStatus(TimeHelper.TimerStatus status)
        {
            base.CurrentStatus = status;
        }
    }
}
