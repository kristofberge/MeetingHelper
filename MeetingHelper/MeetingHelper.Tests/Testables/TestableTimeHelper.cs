﻿using MeetingHelper.Helpers.Time;
using MeetingHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace MeetingHelper.Tests.Testables
{
    internal class TestableTimeHelper : TimeHelper
    {
        public TestableTimeHelper(DispatcherTimer timer) : base(timer) { }

        public void SetCurrentStatus(Common.TimerStatus status)
        {
            base.CurrentStatus = status;
        }

        protected override TimeSpan CalculateTimeToBeDisplayed() { return new TimeSpan(); }

        public bool IsTimerEnabled()
        {
            return Timer.IsEnabled;
        } 
    }
}
