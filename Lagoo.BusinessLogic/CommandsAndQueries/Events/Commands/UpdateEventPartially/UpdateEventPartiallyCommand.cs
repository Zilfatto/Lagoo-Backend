using Lagoo.BusinessLogic.CommandsAndQueries.Events.Common.Dtos;
using Lagoo.Domain.Enums;
using MediatR;

namespace Lagoo.BusinessLogic.CommandsAndQueries.Events.Commands.UpdateEventPartially;

public class UpdateEventPartiallyCommand : IRequest<EventDto>
{
    public long Id { get; set; }
    
    public string? Name { get; set; } = string.Empty;

    public EventType? Type { get; set; }

    public string? Address { get; set; } = string.Empty;

    public string? Comment { get; set; }

    public bool? IsPrivate { get; set; }

    public TimeSpan? Duration { get; set; }

    public DateTime? BeginsAt { get; set; }
}