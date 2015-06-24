using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MeetingHelper.TimeKeepers
{
    public class Stopwatch:BaseTimeKeeper
    {
        public Stopwatch() : base() { }

        protected override TimeSpan CalculateTimeToBeDisplayed()
        {
            return DateTimeOffset.Now - TimeStarted + TimeSpanRunningBeforePause;
        }
    }
}
