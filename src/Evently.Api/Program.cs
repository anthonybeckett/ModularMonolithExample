using Evently.Api.Extensions;
using Evently.Modules.Events.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.ApplyMigrations();
}

EventsModule.MapEndpoints(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
