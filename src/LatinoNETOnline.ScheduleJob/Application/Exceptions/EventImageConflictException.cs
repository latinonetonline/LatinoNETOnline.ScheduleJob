using System;

namespace LatinoNETOnline.ScheduleJob.Application.Exceptions
{

    [Serializable]
    public class EventImageConflictException : Exception
    {
        public EventImageConflictException() { }
        public EventImageConflictException(string message) : base(message) { }
        public EventImageConflictException(string message, Exception inner) : base(message, inner) { }
        protected EventImageConflictException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
