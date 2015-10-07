using MeetingHelper.Helpers;
using MeetingHelper.Helpers.Image;
using MeetingHelper.Helpers.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace MeetingHelper.Shared
{
    public static class MyFactory
    {
        public static IImageHelper GetImageHelper()
        {
            return new ImageHelper(new DialogHandler());
        }

        public static ITimeHelper GetTimeHelper(Common.TimeHelperType type)
        {
            switch (type)
            {
                case Common.TimeHelperType.STOPWATCH:
                    return new Stopwatch(GetDispatcherTimer());
                default:
                    throw new ArgumentException("Wrong TimeHelper type.");
            }
        }

        public static DispatcherTimer GetDispatcherTimer()
        {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 4);
            return timer;
        }
    }
}
