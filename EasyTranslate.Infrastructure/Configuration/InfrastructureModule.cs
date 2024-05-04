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
               .AddSingleton<SearchByNameQuery<CraftAction>>()
               .AddSingleton<SearchByNameQuery<Item>>()
               .AddSingleton<SearchByNameQuery<Status>>()
               .AddSingleton<SearchByNameQuery<Title>>()
               .AddSingleton<SearchByNameQuery<Trait>>();
    }

    private static IServiceCollection AddAdapters(this IServiceCollection serviceCollection)
    {
        return serviceCollection
               .AddSingleton<IContentTypeAdapter<Achievement>, AchievementAdapter>()
               .AddSingleton<IContentTypeAdapter<Action>, ActionAdapter>()
               .AddSingleton<IContentTypeAdapter<CraftAction>, CraftActionAdapter>()
               .AddSingleton<IContentTypeAdapter<Item>, ItemAdapter>()
               .AddSingleton<IContentTypeAdapter<Status>, StatusAdapter>()
               .AddSingleton<IContentTypeAdapter<Title>, TitleAdapter>()
               .AddSingleton<IContentTypeAdapter<Trait>, TraitAdapter>();
    }
}
