using Evently.Modules.Events.Application.Events.GetEvent;

namespace Evently.Modules.Events.Application.Events.SearchEvent;

public sealed record SearchEventsResponse(
	int Page,
	int PageSize,
	int TotalCount,
	IReadOnlyCollection<EventResponse> Events);
