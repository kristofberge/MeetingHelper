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
            TimeToDisplay = CalculateTimeToBeDisplayed();
            OnTimeUpdated();
        }

        protected abstract TimeSpan CalculateTimeToBeDisplayed();
        protected virtual void OnTimeUpdated() { }
    }
}
