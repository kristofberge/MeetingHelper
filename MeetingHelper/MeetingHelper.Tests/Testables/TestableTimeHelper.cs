using MeetingHelper.Helpers.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Tests.Testables
{
    public class TestableTimeHelper : TimeHelper, ITestableTimeHelper
    {
        public void SetCurrentStatus(TimeHelper.TimerStatus status)
        {
            base.CurrentStatus = status;
        }

        protected override TimeSpan CalculateTimeToBeDisplayed()
        {
            throw new NotImplementedException();
        }

        public bool IsTimerEnabled
        {
            get { return Timer.IsEnabled; }
        }
    }
}
