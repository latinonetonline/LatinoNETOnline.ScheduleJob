using System;
using System.Threading;
using System.Threading.Tasks;

using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;

using MediatR;

using Microsoft.Extensions.Logging;

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

            _logger.LogInformation("Finish Test Handler");
        }
    }
}
