using System;
using System.Threading.Tasks;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface ITesseractEngineService
    {
        Task<string> ReadImageText(Uri uri);
    }
}
