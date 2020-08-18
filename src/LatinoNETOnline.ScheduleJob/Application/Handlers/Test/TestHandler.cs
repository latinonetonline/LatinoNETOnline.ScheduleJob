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

        public TestHandler(ILoggerFactory loggerFactory, IEventService eventService, ITelegramService telegramService, IEasyCronService easyCronService)
        {
            _logger = loggerFactory.CreateLogger<TestHandler>();
            _eventService = eventService;
            _telegramService = telegramService;
            _easyCronService = easyCronService;
        }

        protected override async Task Handle(TestRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Test Handler");

            Event @event = await _eventService.GetNextEventAsync();

            _logger.LogInformation($"The Next Event is: {@event.Title}");

            await _telegramService.GetSubscribedChats();

            await _easyCronService.List();

            _logger.LogInformation("Finish Test Handler");
        }
    }
}
