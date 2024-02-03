namespace EasyTranslate.Infrastructure.XivApi.Configuration;

using EasyTranslate.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IItemRepository, XivApiItemRepository>()
            .AddHttpClient(
                XivApiItemRepository.HttpClientName,
                client => client.BaseAddress = new Uri("https://xivapi.com") // TODO add configuration
            );
        return serviceCollection;
    }
}
