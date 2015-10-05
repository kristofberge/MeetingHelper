using MeetingHelper.Helpers.Time;
using MeetingHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Tests.Testables
{
    internal class TestableTimeHelper : TimeHelper
    {
        public void SetCurrentStatus(Common.TimerStatus status)
        {
            base.CurrentStatus = status;
        }

        protected override TimeSpan CalculateTimeToBeDisplayed()
        {
            throw new NotImplementedException();
        }

        public bool CallIsTimerEnabled
        {
            get { return Timer.IsEnabled; }
        } 
    }
}
