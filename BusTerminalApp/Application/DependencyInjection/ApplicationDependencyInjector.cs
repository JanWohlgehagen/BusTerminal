using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ApplicationDependencyInjector
{
    public static void injectApplicationLayer(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBusService, BusService>();
        serviceCollection.AddScoped<IPriceService, PriceService>();
        
    }
    
}