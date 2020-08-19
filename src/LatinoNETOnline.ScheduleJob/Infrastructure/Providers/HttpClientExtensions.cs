using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;

namespace LatinoNETOnline.ScheduleJob.Infrastructure.Providers
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient<ITelegramService, TelegramService>(o =>
            {
                o.BaseAddress = new System.Uri("https://latinonetonlinebot.herokuapp.com/");
            });

            services.AddHttpClient<IEasyCronService, EasyCronService>(o =>
            {
                o.BaseAddress = new System.Uri("https://www.easycron.com/");
            });

            services.AddHttpClient<IOcrService, OcrService>(o =>
            {
                o.BaseAddress = new System.Uri("https://api.ocr.space/");
            });

            return services;
        }
    }
}