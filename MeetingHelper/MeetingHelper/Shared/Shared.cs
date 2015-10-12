using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingHelper.Shared
{
    public class Shared : MeetingHelper.Shared.IShared
    {
        public DateTimeOffset CurrentTime
        {
            get
            {
                return DateTimeOffset.Now;
            }
        }


    }
}
