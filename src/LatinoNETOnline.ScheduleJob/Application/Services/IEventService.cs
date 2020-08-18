using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Domain;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface IEventService
    {
        Task<Event> GetNextEventAsync();
    }
}
