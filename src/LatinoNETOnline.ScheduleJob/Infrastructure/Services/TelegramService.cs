using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain.TelegramBot;

using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly HttpClient _httpclient;
        private readonly IIdentityService _identityService;
        private readonly ILogger<TelegramService> _logger;

        public TelegramService(HttpClient httpclient, IIdentityService identityService, ILoggerFactory loggerFactory)
        {
            _httpclient = httpclient;
            _identityService = identityService;
            _logger = loggerFactory.CreateLogger<TelegramService>();
        }
        public async Task AnnouncementSendNextEvent()
        {
            _logger.LogInformation("Starting Announcement Send Next Event");

            await AddAccessTokenIntoHttpClient();

            var data = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            await _httpclient.PostAsync("api/v1/Announcements/NextEvent", data);

            _logger.LogInformation("The next event was successfully announced by Telegram Bot");
        }

        public async Task<SubscribedChatCollection> GetSubscribedChats()
        {
            _logger.LogInformation("Starting Get Subscribed Chats");

            await AddAccessTokenIntoHttpClient();

            return await _httpclient.GetFromJsonAsync<SubscribedChatCollection>("api/v1/SubscribedChats");
        }

        private async Task AddAccessTokenIntoHttpClient()
        {
            var accessToken = await _identityService.GetAccessToken();

            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}