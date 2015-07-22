using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Helpers.Time
{
    public class Stopwatch : TimeHelper
    {
        protected override TimeSpan CalculateTimeToBeDisplayed()
        {
            return DateTimeOffset.Now - TimeStarted - base.TimeSpanRunningBeforePause;
        }
    }
}
