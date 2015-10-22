using MeetingHelper.Helpers.Time;
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

        public void SetCurrentStatus(Constants.TimerStatus status)
        {
            base.CurrentStatus = status;
        }

        protected override TimeSpan CalculateTimeToDisplay() { return new TimeSpan(); }

        public bool IsTimerEnabled()
        {
            return Timer.IsEnabled;
        } 

        public void CallUpdateTimeToDisplay()
        {
            base.UpdateTimeToDisplay(null, null);
        }

        public void CallOnTimeUpdated()
        {
            base.OnTimeUpdated();
        }

        internal TimeSpan GetTimeRunningBeforePause()
        {
            return base.TimeRunningBeforePause;
        }

        internal DateTimeOffset GetTimeStarted()
        {
            return TimeStarted;
        }
    }
}
