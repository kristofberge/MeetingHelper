using System;
namespace MeetingHelper.Shared
{
    public interface IShared
    {
        DateTimeOffset CurrentTime { get; }
    }
}
