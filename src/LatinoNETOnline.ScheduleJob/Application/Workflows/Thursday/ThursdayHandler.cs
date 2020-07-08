using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LatinoNETOnline.ScheduleJob.Application.Workflows.Thursday
{
    public class ThursdayHandler : AsyncRequestHandler<ThursdayRequest>
    {
        private readonly ILogger<ThursdayHandler> _logger;

        public ThursdayHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ThursdayHandler>();
        }

        protected override Task Handle(ThursdayRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Thursday Workflow");
            return Task.CompletedTask;
        }
    }
}
