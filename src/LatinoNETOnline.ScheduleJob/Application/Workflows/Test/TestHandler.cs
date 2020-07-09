using System.Threading;
using System.Threading.Tasks;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Workflows.Test
{
    public class TestHandler : AsyncRequestHandler<TestRequest>
    {
        private readonly ILogger<TestHandler> _logger;
        private readonly IEventService _eventService;
        private readonly ITelegramService _telegramService;

        public TestHandler(ILoggerFactory loggerFactory, IEventService eventService, ITelegramService telegramService)
        {
            _logger = loggerFactory.CreateLogger<TestHandler>();
            _eventService = eventService;
            _telegramService = telegramService;
        }

        protected override async Task Handle(TestRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Emply Workflow");

            Event @event = await _eventService.GetNextEventAsync();

            _logger.LogInformation($"The Next Event is: {@event.Title}");

            await _telegramService.GetSubscribedChats();

            _logger.LogInformation("Finish Emply Workflow");
        }
    }
}
