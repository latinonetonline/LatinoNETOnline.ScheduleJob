using System;

using LatinoNETOnline.ScheduleJob.Domain.ObjectScheduleds;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.PublishEvent
{
    public class PublishEventRequest : IObjectScheduledRequest
    {
        public Guid ObjectScheduledId { get; set; }
        public Guid Guid { get; set; }
        public DateTime Date { get; set; }
    }
}
