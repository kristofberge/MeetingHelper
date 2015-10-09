using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MeetingHelper.Helpers.Time
{
    public class Stopwatch : TimeHelper
    {
        public Stopwatch(DispatcherTimer timer) : base(timer) { }

        protected override TimeSpan CalculateTimeToDisplay()
        {
            return DateTimeOffset.Now - TimeStarted - TimeSpanRunningBeforePause;
        }
    }
}
