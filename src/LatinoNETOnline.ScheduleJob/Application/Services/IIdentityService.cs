using System.Threading.Tasks;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface IIdentityService
    {
        public Task<string> GetAccessToken();
    }
}
