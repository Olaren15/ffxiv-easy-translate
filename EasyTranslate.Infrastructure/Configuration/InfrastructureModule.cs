namespace EasyTranslate.Infrastructure.Configuration;

using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.Lumina;
using EasyTranslate.Infrastructure.Lumina.Sheets;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IContentRepository, LuminaContentRepository>()
            .AddSingleton<SheetQuery>()
            .AddSingleton<AchievementSheetAdapter>()
            .AddSingleton<ItemSheetAdapter>();

        return serviceCollection;
    }
}
