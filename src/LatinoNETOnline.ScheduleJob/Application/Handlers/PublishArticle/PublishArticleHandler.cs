using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;
using LatinoNETOnline.ScheduleJob.Domain.Blog;
using LatinoNETOnline.ScheduleJob.Domain.Enums;

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
            await AddArticleInArticlesJs(request.Slug);

            var slugDecoded = HttpUtility.UrlDecode(request.Slug);

            await UpdateArticlePublicationStatus(slugDecoded);
        }

        private async Task AddArticleInArticlesJs(string slug)
        {
            FileContent file = await _githubService.GetFileContentAsync(270135101, "db", "articles.js");

            var index = file.Content.IndexOf("[");

            _logger.LogInformation("Old Content: " + Environment.NewLine + file.Content);

            string firstPart = file.Content.Substring(0, index + 1);

            string middlePart = Environment.NewLine + $"    '{slug}',";

            string secondPart = file.Content.Substring(index + 1);

            string complete = firstPart + middlePart + secondPart;

            _logger.LogInformation("New Content: " + Environment.NewLine + complete);

            await _githubService.UpdateFileAsync(270135101, "master", "db", "articles.js", complete);
        }

        private async Task UpdateArticlePublicationStatus(string slug)
        {
            _logger.LogInformation("Updating Article Publication Status to Published");

            _logger.LogInformation("Get GitHub File");

            FileContent file = await _githubService.GetFileContentAsync(258178598, "article", slug);

            _logger.LogInformation($"GitHub File: {file.Name}");

            Article article = JsonSerializer.Deserialize<Article>(file.Content);

            article.PublicationStatus = PublicationStatus.Published;

            await _githubService.UpdateFileAsync(258178598, "master", "article", slug, JsonSerializer.Serialize(article));

            _logger.LogInformation("Updated Article Publication Status to Published");
        }
    }
}
