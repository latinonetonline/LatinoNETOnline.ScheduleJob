using System.Threading.Tasks;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface IJobApplicationService
    {
        Task StartJob();
    }
}