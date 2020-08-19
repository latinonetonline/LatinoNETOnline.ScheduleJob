using System;
using System.Threading.Tasks;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface IOcrService
    {
        Task<string> ReadImageText(Uri uri);
    }
}
