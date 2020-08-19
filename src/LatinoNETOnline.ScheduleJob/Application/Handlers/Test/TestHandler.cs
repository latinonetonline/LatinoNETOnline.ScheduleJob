using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;

using MediatR;

using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.DateTime;

namespace LatinoNETOnline.ScheduleJob.Application.Handlers.Test
{
    public class TestHandler : AsyncRequestHandler<TestRequest>
    {
        private readonly ILogger<TestHandler> _logger;
        private readonly IEventService _eventService;
        private readonly ITelegramService _telegramService;
        private readonly IEasyCronService _easyCronService;
        private readonly IOcrService _ocrSpaceService;

        public TestHandler(ILoggerFactory loggerFactory, IEventService eventService, ITelegramService telegramService, IEasyCronService easyCronService, IOcrService ocrSpaceService)
        {
            _logger = loggerFactory.CreateLogger<TestHandler>();
            _eventService = eventService;
            _telegramService = telegramService;
            _easyCronService = easyCronService;
            _ocrSpaceService = ocrSpaceService;
        }

        protected override async Task Handle(TestRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Test Handler");

            Event @event = await _eventService.GetNextEventAsync();

            _logger.LogInformation($"The Next Event is: {@event.Title}");

            await _telegramService.GetSubscribedChats();

            await _easyCronService.List();

            string text = await _ocrSpaceService.ReadImageText(new Uri(@event.ImageUrl));

            _logger.LogInformation($"Text (GetText): \r\n{text}");

            if (text.ToLower().Contains(@event.Title.ToLower()))
            {
                _logger.LogInformation($"El título `{@event.Title}` se encuentra en la imagen.");
            }
            else
            {
                _logger.LogWarning($"El título `{@event.Title}` no coincide en la imagen.");
            }

            if (text.ToLower().Contains(@event.Speaker.ToLower()))
            {
                _logger.LogInformation($"El speaker `{@event.Speaker}` se encuentra en la imagen.");
            }
            else
            {
                _logger.LogWarning($"El speaker `{@event.Speaker}` no se encuentra en la imagen.");
            }

            var results = DateTimeRecognizer.RecognizeDateTime(text, Culture.Spanish);

            var resultJson = JsonSerializer.Serialize(results);

            _logger.LogInformation($"RecognizeDateTime: \r\n{resultJson}");

            if (resultJson.Contains(@event.Date.ToString("yyyy-dd-MM")))
            {
                _logger.LogInformation($"La fecha `{@event.Date.ToLongDateString()}` se encuentra en la imagen.");
            }
            else
            {
                _logger.LogError($"La fecha `{@event.Date.ToLongDateString()}` no se encuentra en la imagen.");
            }

            

            _logger.LogInformation("Finish Test Handler");
        }
    }
}
