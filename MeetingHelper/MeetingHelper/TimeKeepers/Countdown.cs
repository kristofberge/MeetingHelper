using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MeetingHelper.TimeKeepers
{
    public class Countdown:BaseTimeKeeper
    {
        public event EventHandler CountdownComplete;

        public Countdown(TimeSpan countDownFrom) : base(countDownFrom) { }

        protected override TimeSpan CalculateTimeToBeDisplayed()
        {
            return InitialTime - (DateTimeOffset.Now - TimeStarted + TimeSpanRunningBeforePause);
        }

        protected override void OnTimeUpdated()
        {
            if (TimeToDisplay <= new TimeSpan())
            {
                Timer.Stop();
                if (CountdownComplete != null)
                    CountdownComplete(this, new EventArgs());
            }
        }
    }
}
