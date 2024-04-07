namespace EasyTranslate.Infrastructure.Configuration;

using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.Lumina;
using EasyTranslate.Infrastructure.Lumina.Sheets;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureModule
{
    public const string XivApiUrlConfigName = "XivApiUrl";

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IContentRepository, LuminaContentRepository>().AddSingleton<ItemSheet>();
        return serviceCollection;
    }
}
