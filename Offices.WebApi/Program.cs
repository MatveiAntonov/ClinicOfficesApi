using Microsoft.Extensions.Options;
using Offices.WebApi.DatabaseSettings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.Configure<OfficesDatabaseSettings>(
    builder.Configuration.GetSection(nameof(OfficesDatabaseSettings)));

services.AddSingleton<IOfficesDatabaseSettings>(provider =>
        provider.GetRequiredService<IOptions<OfficesDatabaseSettings>>().Value);
services.AddControllers();

var app = builder.Build();

app.Run();
