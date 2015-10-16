using MeetingHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Events
{
    public class TimeUpdatedEventArgs : EventArgs
    {
        public TimeUpdatedEventArgs(TimeSpan time)
        {
            Time = time;
        }

        public TimeSpan Time { get; private set; }
    }
}
