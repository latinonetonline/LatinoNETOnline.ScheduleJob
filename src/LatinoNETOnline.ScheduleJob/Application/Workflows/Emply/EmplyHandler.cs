using System.Threading;
using System.Threading.Tasks;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Workflows.Emply
{
    public class EmplyHandler : AsyncRequestHandler<EmplyRequest>
    {
        private readonly ILogger<EmplyHandler> _logger;
        private readonly IEventService _eventService;

        public EmplyHandler(ILoggerFactory loggerFactory, IEventService eventService)
        {
            _logger = loggerFactory.CreateLogger<EmplyHandler>();
            _eventService = eventService;
        }

        protected override async Task Handle(EmplyRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Emply Workflow");

            Event @event = await _eventService.GetNextEventAsync();

            _logger.LogInformation($"The Next Event is: {@event.Title}");
        }
    }
}
