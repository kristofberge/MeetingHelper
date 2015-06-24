using MeetingHelper.TimeKeepers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MeetingHelper.TimeKeepers
{
    public enum TimeKeeperStatus
    {
        RUNNING,
        PAUSED,
        STOPPED,
        COMPLETE
    }

    public abstract class BaseTimeKeeper
    {

        protected DispatcherTimer Timer;
        protected DateTimeOffset TimeStarted;
        protected TimeSpan TimeSpanRunningBeforePause;
        protected TimeSpan TimeToDisplay;
        public TimeSpan InitialTime { get; private set; }

        public TimeKeeperStatus Status;

        public delegate void TimeUpdatedEventHandler(object sender, TimeUpdatedEventArgs e);
        public event TimeUpdatedEventHandler TimeUpdated;

        public BaseTimeKeeper(TimeSpan initialTime = new TimeSpan())
        {
            this.InitialTime = initialTime;
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 4);
            Timer.Tick += UpdateTimeToDisplay;
            TimeToDisplay = new TimeSpan();
            Reset();
        }

        public void Start()
        {
            TimeStarted = DateTimeOffset.Now;
            Timer.Start();
            Status = TimeKeeperStatus.RUNNING;
        }

        public void Pause()
        {
            Timer.Stop();
            TimeSpanRunningBeforePause += DateTimeOffset.Now - TimeStarted;
            Status = TimeKeeperStatus.PAUSED;
        }

        public void Resume()
        {
            TimeStarted = DateTimeOffset.Now;
            Timer.Start();
            Status = TimeKeeperStatus.RUNNING;
        }

        public void Reset()
        {
            TimeSpanRunningBeforePause = new TimeSpan();
            if (TimeUpdated != null)
                TimeUpdated(this, new TimeUpdatedEventArgs(InitialTime));
            Status = TimeKeeperStatus.STOPPED;
        }

        private void UpdateTimeToDisplay(object sender, EventArgs args)
        {
            TimeToDisplay = CalculateTimeToBeDisplayed();
            if(TimeUpdated != null)
                TimeUpdated(this, new TimeUpdatedEventArgs(TimeToDisplay));

            OnTimeUpdated();
        }

        protected abstract TimeSpan CalculateTimeToBeDisplayed();
        protected virtual void OnTimeUpdated() { }
    }
}
