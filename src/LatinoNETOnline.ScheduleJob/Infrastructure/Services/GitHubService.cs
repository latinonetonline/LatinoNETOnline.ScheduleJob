using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;
using Octokit;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly IGitHubClient _githubClient;

        public GitHubService(IGitHubClient githubClient)
        {
            _githubClient = githubClient;
        }

        public async Task<FileContent> GetFileContentAsync(long repositoryId, string path, string fileName)
        {
            try
            {
                IReadOnlyList<RepositoryContent> contents = await _githubClient.Repository.Content.GetAllContents(repositoryId, Path.Combine(path, fileName));
                RepositoryContent content = contents.First();
                return new FileContent()
                {
                    Name = content.Name,
                    Content = content.Content,
                    DownloadUrl = content.DownloadUrl
                };
            }
            catch (Octokit.NotFoundException)
            {
                return null;
            }
        }
    }
}
