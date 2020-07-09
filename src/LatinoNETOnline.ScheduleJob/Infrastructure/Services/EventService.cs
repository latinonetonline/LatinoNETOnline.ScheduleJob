using System.Text.Json;
using System.Threading.Tasks;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IGitHubService _githubService;

        public EventService(IGitHubService githubService)
        {
            _githubService = githubService;
        }

        public async Task<Event> GetNextEventAsync()
        {
            FileContent file = await _githubService.GetFileContentAsync(251758832, "events", "NextEvent");
            return JsonSerializer.Deserialize<Event>(file.Content);
        }
    }
}
