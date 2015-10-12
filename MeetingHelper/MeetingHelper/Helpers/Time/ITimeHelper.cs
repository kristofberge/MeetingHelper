using MeetingHelper.Shared;
using System;
namespace MeetingHelper.Helpers.Time
{
    public interface ITimeHelper
    {
        Constants.TimerStatus CurrentStatus { get; }
        void Reset();
        void TimerClicked();
    }
}
