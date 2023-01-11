using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTrackingApp.Infrastructure.Models;

namespace TimeTrackingApp.Infrastructure.Commands.TrackingEvents
{
    public class CreateTrackingEventCommand: IRequest<TrackingEventDto>
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public List<TrackingEventTypeDto> TrackingEventTypes { get; init; }
    }
}
