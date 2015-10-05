using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Shared
{
    public static class Common
    {
        public enum TimerStatus
        {
            STOPPED,
            RUNNING,
            PAUSED
        }

        public enum TimeHelperType
        {
            STOPWATCH,
            COUNTDOWN
        }
    }
}
