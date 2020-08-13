using System;
using System.Threading;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;

using MediatR;

using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.PublishArticle
{
    public class PublishArticleHandler : AsyncRequestHandler<PublishArticleRequest>
    {
        private readonly ILogger<PublishArticleHandler> _logger;
        private readonly IGitHubService _githubService;

        public PublishArticleHandler(ILogger<PublishArticleHandler> logger, IGitHubService githubService)
        {
            _logger = logger;
            _githubService = githubService;
        }

        protected override async Task Handle(PublishArticleRequest request, CancellationToken cancellationToken)
        {
            FileContent file = await _githubService.GetFileContentAsync(270135101, "db", "articles.js");

            var index = file.Content.IndexOf("[");

            _logger.LogInformation("Old Content: " + Environment.NewLine + file.Content);

            string firstPart = file.Content.Substring(0, index + 1);

            string middlePart = Environment.NewLine + $"    '{request.Slug}',";

            string secondPart = file.Content.Substring(index + 1);

            string complete = firstPart + middlePart + secondPart;

            _logger.LogInformation("New Content: " + Environment.NewLine + complete);

            await _githubService.UpdateFileAsync(270135101, "master", "db", "articles.js", complete);
        }
    }
}
