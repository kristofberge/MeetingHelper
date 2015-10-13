using MeetingHelper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Tests.Testables
{
    public class TestableMainViewModel : MainViewModel
    {
        public void CallImageClickedCommand()
        {
            base.ImageClickedCommand();
        }
    }
}
