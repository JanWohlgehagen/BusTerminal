using Application.DependencyInjection;
using Data.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

DataDependencyInjector.InjectDataLayer(builder.Services);
ApplicationDependencyInjector.injectApplicationLayer(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();
