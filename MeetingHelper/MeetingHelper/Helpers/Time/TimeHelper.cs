using MeetingHelper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MeetingHelper.Helpers.Time
{
    public abstract class TimeHelper : ITimeHelper
    {
        public virtual Common.TimerStatus CurrentStatus { get; protected set; }

        protected DispatcherTimer Timer;
        protected DateTimeOffset TimeStarted;
        protected DateTimeOffset TimePaused;
        protected TimeSpan TimeToDisplay;
        protected TimeSpan TimeSpanRunningBeforePause;

        public TimeHelper()
        {
            SetUpTimer();
            TimeToDisplay = new TimeSpan();
            TimeStarted = new DateTimeOffset();
            TimePaused = new DateTimeOffset();
            TimeSpanRunningBeforePause = new TimeSpan();
        }

        private void SetUpTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 4);
            Timer.Tick += UpdateTimeToDisplay;
        }

        #region Controls
        public void TimerClicked()
        {
            switch (this.CurrentStatus)
            {
                case Common.TimerStatus.STOPPED:
                    Start();
                    break;
                case Common.TimerStatus.RUNNING:
                    Pause();
                    break;
                case Common.TimerStatus.PAUSED:
                    Resume();
                    break;
                default:
                    throw new ArgumentException("Incorrect status.");
            }
        }

        private void Start()
        {
            TimeStarted = DateTimeOffset.Now;
            Timer.Start();
            this.CurrentStatus = Common.TimerStatus.RUNNING;
        }

        private void Pause()
        {
            Timer.Stop();
            this.CurrentStatus = Common.TimerStatus.PAUSED;
        }

        private void Resume()
        {
            Timer.Start();
            this.CurrentStatus = Common.TimerStatus.RUNNING;
        }

        public void Reset()
        {
            this.CurrentStatus = Common.TimerStatus.STOPPED;
        }
        #endregion

        private void UpdateTimeToDisplay(object sender, EventArgs args)
        {
            TimeToDisplay = CalculateTimeToBeDisplayed();
            OnTimeUpdated();
        }

        protected abstract TimeSpan CalculateTimeToBeDisplayed();
        protected virtual void OnTimeUpdated() { }
    }
}
