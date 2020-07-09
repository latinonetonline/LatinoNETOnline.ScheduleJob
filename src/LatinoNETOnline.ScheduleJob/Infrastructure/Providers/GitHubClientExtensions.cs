using GitHubActionSharp;
using LatinoNETOnline.ScheduleJob.Application.Enums;
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

            return services;
        }
    }
}
