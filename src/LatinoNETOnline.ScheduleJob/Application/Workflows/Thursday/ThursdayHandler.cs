using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Workflows.Thursday
{
    public class ThursdayHandler : AsyncRequestHandler<ThursdayRequest>
    {
        private readonly ILogger<ThursdayHandler> _logger;
        private readonly IEventService _eventService;
        private readonly ITwitterService _twitterService;
        private readonly HttpClient _httpClient;

        public ThursdayHandler(ILoggerFactory loggerFactory, IEventService eventService, ITwitterService twitterService, IHttpClientFactory httpClientFactory)
        {
            _logger = loggerFactory.CreateLogger<ThursdayHandler>();
            _eventService = eventService;
            _twitterService = twitterService;
            _httpClient = httpClientFactory.CreateClient();
        }

        protected override async Task Handle(ThursdayRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Thursday Workflow");

            Event @event = await _eventService.GetNextEventAsync();

            _logger.LogInformation($"The Next Event is: {@event.Title}");

            byte[] image = await _httpClient.GetByteArrayAsync("https://eoimages.gsfc.nasa.gov/images/imagerecords/144000/144269/osirisrexview_earthmoon_201817.jpg");

            Uri tweetUri = await _twitterService.CreateTweet("test", image);

            _logger.LogInformation($"Tweet created: {tweetUri}");
        }
    }
}
