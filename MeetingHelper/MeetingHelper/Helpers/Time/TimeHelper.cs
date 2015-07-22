using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MeetingHelper.Helpers.Time
{
    public abstract class TimeHelper
    {
        public virtual TimerStatus CurrentStatus { get; protected set; }

        public enum TimerStatus
        {
            STOPPED,
            RUNNING,
            PAUSED
        }

        protected DispatcherTimer Timer;
        protected DateTimeOffset TimeStarted;
        protected TimeSpan TimeToDisplay;
        protected TimeSpan TimeSpanRunningBeforePause;

        public TimeHelper()
        {
            SetUpTimer();
            TimeToDisplay = new TimeSpan();
            TimeStarted = new DateTimeOffset();
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
                case TimerStatus.STOPPED:
                    Start();
                    break;
                case TimerStatus.RUNNING:
                    Pause();
                    break;
                case TimerStatus.PAUSED:
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
            this.CurrentStatus = TimerStatus.RUNNING;
        }

        private void Pause()
        {
            this.CurrentStatus = TimerStatus.PAUSED;
        }

        private void Resume()
        {
            this.CurrentStatus = TimerStatus.RUNNING;
        }

        public void Reset()
        {
            this.CurrentStatus = TimerStatus.STOPPED;
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
