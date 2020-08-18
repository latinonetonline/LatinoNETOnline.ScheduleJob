using GitHubActionSharp;

using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;

using Octokit;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Providers
{
    public static class GitHubClientExtensions
    {
        public static IServiceCollection AddGitHubClient(this IServiceCollection services, GitHubActionContext gitHubActionContext)
        {
            string token = gitHubActionContext.GetParameter(Parameters.GitHubAccessToken);
            GitHubClient githubClient = new GitHubClient(new ProductHeaderValue(nameof(LatinoNETOnline)));
            Credentials basicAuth = new Credentials(token);
            githubClient.Credentials = basicAuth;

            services.AddSingleton<IGitHubClient, GitHubClient>(service => githubClient);
            services.AddSingleton<IGitHubService, GitHubService>();

            return services;
        }
    }
}
