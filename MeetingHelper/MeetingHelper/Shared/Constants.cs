using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Shared
{
    public static class Constants
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

        public const string TIME_FORMAT_MASK = @"hh\:mm\:ss\.ff";
    }
}
