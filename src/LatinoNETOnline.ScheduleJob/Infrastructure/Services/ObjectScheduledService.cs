using System;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class ObjectScheduledService : IObjectScheduledService
    {
        private readonly IGitHubService _githubService;

        public ObjectScheduledService(IGitHubService githubService)
        {
            _githubService = githubService;
        }

        public Task<FileContent> GetObjectScheduledAsync(Guid objectScheduledId)
        {
            return _githubService.GetFileContentAsync(279726574, "objectscheduled", objectScheduledId.ToString());
        }

        public Task DeleteObjectScheduledAsync(Guid objectScheduledId)
        {
            return _githubService.DeleteFileAsync(279726574, "objectscheduled", objectScheduledId.ToString());
        }
    }
}
