using MeetingHelper.Helpers.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Tests.Testables
{
    public interface ITestableTimeHelper
    {
        void SetCurrentStatus(TimeHelper.TimerStatus status);
    }
}
