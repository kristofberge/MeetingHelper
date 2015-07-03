using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Helpers.TimeKeerpers
{
    public class TimeHelper
    {
        public virtual TimerStatus CurrentStatus { get; protected set; }

        public enum TimerStatus
        {
            STOPPED,
            RUNNING,
            PAUSED
        }

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
    }
}
