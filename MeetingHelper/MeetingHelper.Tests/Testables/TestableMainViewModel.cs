using MeetingHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingHelper.Events;

namespace MeetingHelper.Tests.Testables
{
    public class TestableMainViewModel : MainViewModel
    {
        internal void CallImageClickedCommand()
        {
            base.ImageClickedCommand();
        }

        internal void CallTimerClickedCommand()
        {
            base.TimerClickedCommand();
        }

        internal object CallCreateDisplayTime(TimeSpan time)
        {
            return CreateDisplayTime(time);
        }

        internal void CallUpdateDisplayTime(object sender, TimeUpdatedEventArgs eventArgs)
        {
            UpdateDisplayTime(sender, eventArgs);
        }
    }
}
