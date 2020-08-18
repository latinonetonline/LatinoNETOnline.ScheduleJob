using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Domain;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface IGitHubService
    {
        Task<FileContent> GetFileContentAsync(long repositoryId, string path, string fileName);
        Task DeleteFileAsync(long repositoryId, string path, string fileName);
        Task UpdateFileAsync(long repositoryId, string master, string path, string fileName, string content);
    }
}
