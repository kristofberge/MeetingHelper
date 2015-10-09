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

        protected virtual DispatcherTimer Timer { get; set; }
        protected virtual DateTimeOffset TimeStarted { get; set; }
        protected virtual DateTimeOffset TimePaused { get; set; }
        protected virtual TimeSpan TimeToDisplay { get; set; }
        protected virtual TimeSpan TimeSpanRunningBeforePause { get; set; }

        #region Constructors
        public TimeHelper(DispatcherTimer timer)
        {
            Timer = timer;
            Timer.Tick += UpdateTimeToDisplay;
            SetUpTimeVariables();
        }

        private void SetUpTimeVariables()
        {
            TimeToDisplay = new TimeSpan();
            TimeStarted = new DateTimeOffset();
            TimePaused = new DateTimeOffset();
            TimeSpanRunningBeforePause = new TimeSpan();
        }
        #endregion

        #region Controls
        public void TimerClicked()
        {
            switch (CurrentStatus)
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
            CurrentStatus = Common.TimerStatus.RUNNING;
        }

        private void Pause()
        {
            Timer.Stop();
            CurrentStatus = Common.TimerStatus.PAUSED;
        }

        private void Resume()
        {
            Timer.Start();
            CurrentStatus = Common.TimerStatus.RUNNING;
        }

        public void Reset()
        {
            CurrentStatus = Common.TimerStatus.STOPPED;
        }
        #endregion

        protected virtual void UpdateTimeToDisplay(object sender, EventArgs args)
        {
            TimeToDisplay = CalculateTimeToDisplay();
            OnTimeUpdated();
        }

        protected abstract TimeSpan CalculateTimeToDisplay();
        protected virtual void OnTimeUpdated() { }
    }
}
