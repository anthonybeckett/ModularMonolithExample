using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

internal static class CreateEvent
{
	public static void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("events", async (Request request, ISender sender) =>
			{
				Result<Guid> result = await sender.Send(new CreateEventCommand(
					request.CategoryId,
					request.Title,
					request.Description,
					request.Location,
					request.StartsAtUtc,
					request.EndsAtUtc));

				return result.Match(Results.Ok, ApiResults.ApiResults.Problem);
			})
			.WithTags(Tags.Events);
	}

	internal sealed class Request
	{
		public Guid CategoryId { get; init; }

		public required string Title { get; init; }

		public required string Description { get; init; }

		public required string Location { get; init; }

		public DateTime StartsAtUtc { get; init; }

		public DateTime? EndsAtUtc { get; init; }
	}
}
