using Application.Interfaces;
using Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data.DependencyInjection;

public static class DataDependencyInjector
{
    public static void InjectDataLayer(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBusRepository, BusRespository>();
        
    }
    
}