using Evently.Common.Application.EventBus;

namespace Evently.Modules.Users.IntegrationEvents;

public sealed class UserRegisteredIntegrationEvent(
	Guid id,
	DateTime occuredOnUtc,
	Guid userId,
	string email,
	string firstName,
	string lastName) : IntegrationEvent(id, occuredOnUtc)
{
	public Guid UserId { get; init; } = userId;
	public string Email { get; set; } = email;
	public string FirstName { get; set; } = firstName;
	public string LastName { get; set; } = lastName;
}
