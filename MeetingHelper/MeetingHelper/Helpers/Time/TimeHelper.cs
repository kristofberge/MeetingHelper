using MeetingHelper.Events;
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
        public virtual Constants.TimerStatus CurrentStatus { get; protected set; }

        protected virtual DispatcherTimer Timer { get; set; }
        protected virtual DateTimeOffset TimeStarted { get; set; }
        protected virtual TimeSpan TimeToDisplay { get; set; }
        protected virtual TimeSpan TimeRunningBeforePause { get; set; }
        protected virtual IShared Shared { get; set; }

        #region Constructors
        public TimeHelper(DispatcherTimer timer)
        {
            Timer = timer;
            Timer.Tick += UpdateTimeToDisplay;
            Shared = new Shared.Shared();
            SetUpTimeVariables();
        }

        private void SetUpTimeVariables()
        {
            TimeToDisplay = new TimeSpan();
            TimeRunningBeforePause = new TimeSpan();
        }
        #endregion

        #region Controls
        public void TimerClicked()
        {
            switch (CurrentStatus)
            {
                case Constants.TimerStatus.STOPPED:
                    Start();
                    break;
                case Constants.TimerStatus.RUNNING:
                    Pause();
                    break;
                case Constants.TimerStatus.PAUSED:
                    Resume();
                    break;
                default:
                    throw new ArgumentException("Incorrect status.");
            }
        }

        private void Start()
        {
            TimeStarted = Shared.CurrentTime;
            Timer.Start();
            CurrentStatus = Constants.TimerStatus.RUNNING;
        }

        private void Pause()
        {
            Timer.Stop();
            TimeRunningBeforePause = Shared.CurrentTime - TimeStarted;
            CurrentStatus = Constants.TimerStatus.PAUSED;
        }

        private void Resume()
        {
            Timer.Start();
            CurrentStatus = Constants.TimerStatus.RUNNING;
        }

        public void Reset()
        {
            CurrentStatus = Constants.TimerStatus.STOPPED;
        }
        #endregion

        public virtual event EventHandler<TimeUpdatedEventArgs> TimeUpdated;

        protected virtual void UpdateTimeToDisplay(object sender, EventArgs args)
        {
            TimeToDisplay = CalculateTimeToDisplay();
            OnTimeUpdated();
        }

        protected abstract TimeSpan CalculateTimeToDisplay();
        protected virtual void OnTimeUpdated()
        {
            if (TimeUpdated != null)
                TimeUpdated(this, new TimeUpdatedEventArgs(TimeToDisplay));
        }
    }
}
