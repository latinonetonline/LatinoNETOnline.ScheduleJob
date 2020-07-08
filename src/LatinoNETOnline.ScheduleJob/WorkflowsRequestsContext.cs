using System;
using System.Collections.Generic;
using LatinoNETOnline.ScheduleJob.Application.Workflows.Thursday;
using MediatR;

namespace LatinoNETOnline.ScheduleJob
{
    public class WorkflowsRequestsContext
    {

        public IRequest GetRequest(Enums.Workflows workflow)
        {
            return Pairs[workflow];
        }

        public IRequest GetRequest(string workflow)
        {
            Enums.Workflows workflowEnum = Enum.Parse<Enums.Workflows>(workflow);
            return Pairs[workflowEnum];
        }

        private readonly IReadOnlyDictionary<Enums.Workflows, IRequest> Pairs = new Dictionary<Enums.Workflows, IRequest>
            {
                { Enums.Workflows.Thursday, new ThursdayRequest() }
            };
    }
}
