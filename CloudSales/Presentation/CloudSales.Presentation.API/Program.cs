using CloudSales.Application.Services;
using CloudSales.Infrastructure.Repositories;
using CloudSales.Presentation.API.Exceptions;
using CloudSales.Infrastructure.Database;
using Serilog;
using CloudSales.Infrastructure.Ccp;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseInfrastructure(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCcpClient();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.MapControllers();

await app.RunAsync();
