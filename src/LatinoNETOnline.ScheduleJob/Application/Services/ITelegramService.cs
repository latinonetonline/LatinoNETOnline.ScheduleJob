using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Domain.TelegramBot;

namespace LatinoNETOnline.ScheduleJob.Application.Services
{
    public interface ITelegramService
    {
        Task AnnouncementSendNextEvent();
        Task<SubscribedChatCollection> GetSubscribedChats();
    }
}