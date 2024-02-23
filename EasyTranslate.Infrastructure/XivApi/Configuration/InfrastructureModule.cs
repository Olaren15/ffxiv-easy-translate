namespace EasyTranslate.Infrastructure.XivApi.Configuration;

using EasyTranslate.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureModule
{
    public const string XivApiUrlConfigName = "XivApiUrl";
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration config)
    {
        serviceCollection
            .AddSingleton<IItemRepository, XivApiItemRepository>()
            .AddHttpClient(
                XivApiItemRepository.HttpClientName,
                client => client.BaseAddress = new Uri(config[XivApiUrlConfigName]!)
            );
        return serviceCollection;
    }
}
