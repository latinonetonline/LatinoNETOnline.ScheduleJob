using System;

using LatinoNETOnline.ScheduleJob.Domain.ObjectScheduleds;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.Social
{
    public class SocialRequest : IObjectScheduledRequest
    {
        public Guid ObjectScheduledId { get; set; }
    }
}
