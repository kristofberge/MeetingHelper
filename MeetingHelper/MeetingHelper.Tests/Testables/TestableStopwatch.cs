﻿using MeetingHelper.Helpers.Time;
using MeetingHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MeetingHelper.Tests.Testables
{
    public class TestableStopwatch : Stopwatch
    {
        public TestableStopwatch(DispatcherTimer timer) : base(timer) { }

        public TimeSpan CallCalculateTimeToBeDisplayed()
        {
            return base.CalculateTimeToDisplay();
        }

        public void SetCurrentStatus(Constants.TimerStatus status)
        {
            base.CurrentStatus = status;
        }
    }
}
