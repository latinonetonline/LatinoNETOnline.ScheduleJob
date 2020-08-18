using System;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Domain;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface IObjectScheduledService
    {
        Task<FileContent> GetObjectScheduledAsync(Guid objectScheduledId);
        Task DeleteObjectScheduledAsync(Guid objectScheduledId);
    }
}
