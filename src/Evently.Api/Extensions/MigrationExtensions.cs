using Evently.Modules.Events.Api.Database;
using Evently.Modules.Events.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Api.Extensions;

internal static class MigrationExtensions
{
	internal static void ApplyMigrations(this IApplicationBuilder app)
	{
		using IServiceScope scope = app.ApplicationServices.CreateScope();
		
		ApplyMigration<EventsDbContext>(scope);
	}

	private static void ApplyMigration<TDbContext>(IServiceScope scope) where TDbContext : DbContext
	{
		using TDbContext dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
		
		dbContext.Database.Migrate();
	}
}
