using Microsoft.Extensions.Options;
using Offices.Application.DatabaseSettings;
using Offices.Application.Interfaces;
using Offices.Application.Repositories;
using Offices.WebApi.Mappings;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

services.Configure<OfficesDatabaseSettings>(
    builder.Configuration.GetSection(nameof(OfficesDatabaseSettings)));

services.AddSingleton<IOfficesDatabaseSettings>(provider =>
        provider.GetRequiredService<IOptions<OfficesDatabaseSettings>>().Value);

services.AddScoped<IOfficeRepository, OfficeRepository>();

services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

app.UseRouting();

app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
