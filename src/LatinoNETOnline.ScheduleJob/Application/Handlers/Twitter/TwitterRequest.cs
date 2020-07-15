using System;
using LatinoNETOnline.ScheduleJob.Domain.ObjectScheduleds;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.Twitter
{
    public class TwitterRequest : IObjectScheduledRequest
    {
        public Guid ObjectScheduledId { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
    }
}
