namespace EasyTranslate.Infrastructure.Configuration;

using Domain.Repositories;
using GameData;
using GameData.Adapters;
using Lumina.Excel.GeneratedSheets2;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
               .AddSingleton<IContentRepository, GameDataContentRepository>()
               .AddQueries()
               .AddAdapters();
    }

    private static IServiceCollection AddQueries(this IServiceCollection serviceCollection)
    {
        return serviceCollection
               .AddSingleton<SearchByNameQuery<Achievement>>()
               .AddSingleton<SearchByNameQuery<Action>>()
               .AddSingleton<SearchByNameQuery<Item>>()
               .AddSingleton<SearchByNameQuery<Title>>();
    }

    private static IServiceCollection AddAdapters(this IServiceCollection serviceCollection)
    {
        return serviceCollection
               .AddSingleton<IContentTypeAdapter<Achievement>, AchievementAdapter>()
               .AddSingleton<IContentTypeAdapter<Action>, ActionAdapter>()
               .AddSingleton<IContentTypeAdapter<Item>, ItemAdapter>()
               .AddSingleton<IContentTypeAdapter<Title>, TitleAdapter>();
    }
}
