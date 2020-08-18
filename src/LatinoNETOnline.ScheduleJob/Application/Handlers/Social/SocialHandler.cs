using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Services;

using MediatR;

using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.Social
{
    public class SocialHandler : AsyncRequestHandler<SocialRequest>
    {
        private readonly ILogger<SocialHandler> _logger;
        private readonly IEventService _eventService;
        private readonly ITwitterService _twitterService;
        private readonly HttpClient _httpClient;
        private readonly ITelegramService _telegramService;

        public SocialHandler(ILoggerFactory loggerFactory, IEventService eventService, ITwitterService twitterService, IHttpClientFactory httpClientFactory, ITelegramService telegramService)
        {
            _logger = loggerFactory.CreateLogger<SocialHandler>();
            _eventService = eventService;
            _twitterService = twitterService;
            _httpClient = httpClientFactory.CreateClient();
            _telegramService = telegramService;
        }

        protected override Task Handle(SocialRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
