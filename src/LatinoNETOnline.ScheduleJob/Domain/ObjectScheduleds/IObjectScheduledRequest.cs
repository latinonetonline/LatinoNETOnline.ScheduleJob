using System;
using MediatR;

namespace LatinoNETOnline.ScheduleJob.Domain.ObjectScheduleds
{
    public interface IObjectScheduledRequest : IRequest
    {
        public Guid ObjectScheduledId { get; set; }
    }
}
