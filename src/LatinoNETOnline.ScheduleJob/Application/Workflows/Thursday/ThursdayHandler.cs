using System.Threading;
using System.Threading.Tasks;
using LatinoNETOnline.ScheduleJob.Application.Services;
using LatinoNETOnline.ScheduleJob.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Workflows.Thursday
{
    public class ThursdayHandler : AsyncRequestHandler<ThursdayRequest>
    {
        private readonly ILogger<ThursdayHandler> _logger;
        private readonly IEventService _eventService;

        public ThursdayHandler(ILoggerFactory loggerFactory, IEventService eventService)
        {
            _logger = loggerFactory.CreateLogger<ThursdayHandler>();
            _eventService = eventService;
        }

        protected override async Task Handle(ThursdayRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Thursday Workflow");
            Event @event = await _eventService.GetNextEventAsync();

            _logger.LogInformation("Starting Thursday Workflow");
        }
    }
}
