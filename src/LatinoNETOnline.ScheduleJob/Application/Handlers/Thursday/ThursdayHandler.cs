using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Enums;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;

using MediatR;

using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.Thursday
{
    public class ThursdayHandler : AsyncRequestHandler<ThursdayRequest>
    {
        private readonly ILogger<ThursdayHandler> _logger;
        private readonly IEventService _eventService;
        private readonly ITwitterService _twitterService;
        private readonly HttpClient _httpClient;
        private readonly ITelegramService _telegramService;

        public ThursdayHandler(ILoggerFactory loggerFactory, IEventService eventService, ITwitterService twitterService, IHttpClientFactory httpClientFactory, ITelegramService telegramService)
        {
            _logger = loggerFactory.CreateLogger<ThursdayHandler>();
            _eventService = eventService;
            _twitterService = twitterService;
            _httpClient = httpClientFactory.CreateClient();
            _telegramService = telegramService;
        }

        protected override async Task Handle(ThursdayRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Thursday Handler");

            Task telegramTask = _telegramService.AnnouncementSendNextEvent();

            Event @event = await _eventService.GetNextEventAsync();

            _logger.LogInformation($"The Next Event is: {@event.Title}");

            Random random = new Random();
            int nroRandom = random.Next(1, 4);

            string tweetText = nroRandom switch
            {
                1 => BuildTweetText1(@event),
                2 => BuildTweetText2(@event),
                3 => BuildTweetText3(@event),
                _ => BuildTweetText1(@event)
            };

            byte[] image = await _httpClient.GetByteArrayAsync(@event.ImageUrl);

            Uri tweetUri = await _twitterService.CreateTweet(tweetText, image);

            _logger.LogInformation($"Tweet created: {tweetUri}");

            Task.WaitAll(telegramTask);

            _logger.LogInformation("Finish Thursday Handler");
        }

        string BuildTweetText1(Event @event)
        {
            string speaker = string.IsNullOrWhiteSpace(@event.TwitterSpeaker) ? @event.Speaker : @event.TwitterSpeaker;
            string date = $"{(DayOfWeekSpanish)@event.Date.DayOfWeek} 📅 {@event.Date.Day} de {(Month)@event.Date.Month}";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("⚠️¡¡ALERTA WEBINAR!!⚠️");
            sb.AppendLine($"El {date} a las 🕒 {@event.Date.Hour}hs UTC junto a 🤝 {speaker} les traemos una interesante charla titulada 📚 {@event.Title}.");
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine("¡No lo dejes pasar! 👉👉 Regístrate en https://latinonet.online/links#registro");
            return sb.ToString();
        }

        string BuildTweetText2(Event @event)
        {
            string speaker = string.IsNullOrWhiteSpace(@event.TwitterSpeaker) ? @event.Speaker : @event.TwitterSpeaker;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("⛔¡¡ATENCIÓN NUEVO WEBINAR!!⛔");
            sb.AppendLine($"Este {(DayOfWeekSpanish)@event.Date.DayOfWeek} {@event.Date.Day} a las {@event.Date.Hour} 🕒 horas UTC, {speaker} nos va a compartir 📚 {@event.Title}.");
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine("¡Agendalo! Inscríbete aquí 👇👇");
            sb.AppendLine("https://latinonet.online/links#registro");
            return sb.ToString();
        }

        string BuildTweetText3(Event @event)
        {
            string speaker = string.IsNullOrWhiteSpace(@event.TwitterSpeaker) ? @event.Speaker : @event.TwitterSpeaker;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("🚨¡¡PROXIMO WEBINAR!! 🚨");
            sb.AppendLine($"Como todas las semanas 😎, traemos un nuevo webinar.");
            sb.AppendLine($" De la mano de 👉 {speaker} presentamos 📚 {@event.Title}, este {(DayOfWeekSpanish)@event.Date.DayOfWeek} 📅 {@event.Date.Day} a las {@event.Date.Hour} hs 🕒 UTC.");
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine("¡No te lo podes perder! Inscríbete 👇👇");
            sb.AppendLine("https://latinonet.online/links#registro");
            return sb.ToString();
        }
    }
}
