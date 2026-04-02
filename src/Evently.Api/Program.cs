using Evently.Api.Extensions;
using Evently.Api.Middleware;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Evently.Modules.Events.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddApplication([Evently.Modules.Events.Application.AssemblyReference.Assembly]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

builder.Services.AddInfrastructure(
	databaseConnectionString,
	redisConnectionString
);

builder.Configuration.AddModuleConfiguration([
	"events"
]);

builder.Services.AddHealthChecks()
	.AddNpgSql(databaseConnectionString)
	.AddRedis(redisConnectionString);

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.ApplyMigrations();
}

EventsModule.MapEndpoints(app);

app.MapHealthChecks("health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
