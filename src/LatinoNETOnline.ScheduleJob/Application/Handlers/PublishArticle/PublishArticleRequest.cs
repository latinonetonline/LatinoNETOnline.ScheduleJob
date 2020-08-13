using System;

using LatinoNETOnline.ScheduleJob.Domain.ObjectScheduleds;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.PublishArticle
{
    public class PublishArticleRequest : IObjectScheduledRequest
    {
        public Guid ObjectScheduledId { get; set; }
        public string Slug { get; set; }
    }
}
