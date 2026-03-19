using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.SearchEvent;

public sealed record SearchEventsQuery(
	Guid? CategoryId,
	DateTime? StartDate,
	DateTime? EndDate,
	int Page,
	int PageSize) : IQuery<SearchEventsResponse>;

