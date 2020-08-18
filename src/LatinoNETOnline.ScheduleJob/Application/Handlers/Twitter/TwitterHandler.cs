using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Services;

using MediatR;

using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.Twitter
{
    public class TwitterHandler : AsyncRequestHandler<TwitterRequest>
    {
        private readonly ILogger<TwitterHandler> _logger;
        private readonly ITwitterService _twitterService;
        private readonly HttpClient _httpClient;

        public TwitterHandler(ILoggerFactory loggerFactory, ITwitterService twitterService, IHttpClientFactory httpClientFactory)
        {
            _logger = loggerFactory.CreateLogger<TwitterHandler>();
            _twitterService = twitterService;
            _httpClient = httpClientFactory.CreateClient();
        }

        protected override async Task Handle(TwitterRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Twitter Handler");

            byte[] image = string.IsNullOrWhiteSpace(request.ImageUrl) ? null : await _httpClient.GetByteArrayAsync(request.ImageUrl);

            Uri tweetUri = await _twitterService.CreateTweet(request.Text, image);

            _logger.LogInformation($"Tweet created: {tweetUri}");

            _logger.LogInformation("Finish Twitter Handler");
        }
    }
}
