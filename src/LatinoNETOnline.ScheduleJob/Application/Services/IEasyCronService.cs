using System.Threading.Tasks;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface IEasyCronService
    {
        Task DisableJob(long cronId);
        Task List();
    }
}
