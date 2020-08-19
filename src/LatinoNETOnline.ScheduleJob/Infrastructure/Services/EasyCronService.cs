using System.Net.Http;
using System.Threading.Tasks;

using GitHubActionSharp;

using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Services;

using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Services
{
    public class EasyCronService : IEasyCronService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EasyCronService> _logger;
        private readonly string _easyCronClientSecret;

        public EasyCronService(HttpClient httpclient, GitHubActionContext gitHubActionContext, ILoggerFactory loggerFactory)
        {
            _httpClient = httpclient;
            _logger = loggerFactory.CreateLogger<EasyCronService>();
            _easyCronClientSecret = gitHubActionContext.GetParameter(Parameters.EasyCronClientSecret);
        }

        public async Task DisableJob(long cronId)
        {
            _logger.LogInformation("Starting Disable Cron Job List");
            var httpResponse = await _httpClient.GetAsync($"rest/disable?token={_easyCronClientSecret}&id={cronId}");
            httpResponse.EnsureSuccessStatusCode();
        }

        public async Task List()
        {
            _logger.LogInformation("Starting Get Cron Job List");

            var httpResponse = await _httpClient.GetAsync($"rest/list?token={_easyCronClientSecret}");
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
