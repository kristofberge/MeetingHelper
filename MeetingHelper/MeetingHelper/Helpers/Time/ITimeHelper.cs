using MeetingHelper.Shared;
using System;
namespace MeetingHelper.Helpers.Time
{
    public interface ITimeHelper
    {
        Common.TimerStatus CurrentStatus { get; }
        void Reset();
        void TimerClicked();
    }
}
