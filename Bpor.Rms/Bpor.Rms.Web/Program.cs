using Bpor.Rms.Web.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddApplicationConfiguration();
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.ConfigureDatabase(builder.Configuration);

var app = builder.Build();

app.UseApplicationMiddleware();

app.Run();
