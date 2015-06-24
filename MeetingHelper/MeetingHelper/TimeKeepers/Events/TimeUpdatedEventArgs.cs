﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.TimeKeepers.Events
{
    public class TimeUpdatedEventArgs : EventArgs
    {
        public TimeUpdatedEventArgs(TimeSpan time)
        {
            this.Time = time;
        }

        public TimeSpan Time { get; private set; }

        public string TimeAsString
        {
            get
            {
                return this.Time.ToString(Constants.TIME_FORMAT_MASK);

            }
        }
    }
}
