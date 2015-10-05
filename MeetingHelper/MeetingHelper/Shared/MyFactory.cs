using MeetingHelper.Helpers;
using MeetingHelper.Helpers.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MeetingHelper.Shared
{
    public static class MyFactory
    {
        public static IImageHelper GetImageHelper()
        {
            return new ImageHelper();
        }

        public static ITimeHelper GetTimeHelper(Common.TimeHelperType type)
        {
            switch (type)
            {
                case Common.TimeHelperType.STOPWATCH:
                    return new Stopwatch();
                default:
                    throw new ArgumentException("Wrong TimeHelper type.");
            }
        }
    }
}
