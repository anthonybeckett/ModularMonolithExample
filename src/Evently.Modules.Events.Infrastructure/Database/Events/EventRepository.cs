using Evently.Modules.Events.Domain.Events;

namespace Evently.Modules.Events.Infrastructure.Database.Events;

internal sealed class EventRepository(EventsDbContext context) : IEventRepository
{
	public void Insert(Event @event)
	{
		context.Events.Add(@event);
	}
}
