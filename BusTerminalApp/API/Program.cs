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

if (app.Environment.IsDevelopment())
{
    var url = "http://localhost:5267/swagger";
    Console.WriteLine($"Opening Swagger at: {url}");
    try
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to open browser: {ex.Message}");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();
