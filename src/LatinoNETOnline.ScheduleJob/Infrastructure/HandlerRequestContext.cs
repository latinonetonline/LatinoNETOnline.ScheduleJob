using System;
using System.Collections.Generic;

using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Handlers.PublishArticle;
using LatinoNETOnline.ScheduleJob.Application.Handlers.Social;
using LatinoNETOnline.ScheduleJob.Application.Handlers.Test;
using LatinoNETOnline.ScheduleJob.Application.Handlers.Thursday;
using LatinoNETOnline.ScheduleJob.Application.Handlers.Twitter;

using MediatR;

namespace LatinoNETOnline.ScheduleJob.Infrastructure
{
    public class HandlerRequestContext
    {
        public IRequest GetRequest(HandlerName handlerName)
        {
            return Pairs[handlerName];
        }

        public IRequest GetRequest(string handlerName)
        {
            HandlerName handlerNameEnum = Enum.Parse<HandlerName>(handlerName);
            return Pairs[handlerNameEnum];
        }

        private readonly IReadOnlyDictionary<HandlerName, IRequest> Pairs = new Dictionary<HandlerName, IRequest>
            {
                { HandlerName.Test, new TestRequest() },
                { HandlerName.Thursday, new ThursdayRequest() },
                { HandlerName.Social, new SocialRequest() },
                { HandlerName.Twitter, new TwitterRequest() },
                { HandlerName.PublishArticle, new PublishArticleRequest() }
            };
    }
}
