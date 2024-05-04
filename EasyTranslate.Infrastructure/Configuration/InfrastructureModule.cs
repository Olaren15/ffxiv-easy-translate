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
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Achievement>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Action>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<BNpcName>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Companion>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<CraftAction>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<ENpcResident>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Item>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Mount>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Status>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Title>>()
               .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Trait>>();
    }

    private static IServiceCollection AddAdapters(this IServiceCollection serviceCollection)
    {
        return serviceCollection
               .AddSingleton<IContentTypeAdapter<Achievement>, AchievementAdapter>()
               .AddSingleton<IContentTypeAdapter<Action>, ActionAdapter>()
               .AddSingleton<IContentTypeAdapter<BNpcName>, BNpcNameAdapter>()
               .AddSingleton<IContentTypeAdapter<Companion>, CompanionAdapter>()
               .AddSingleton<IContentTypeAdapter<CraftAction>, CraftActionAdapter>()
               .AddSingleton<IContentTypeAdapter<ENpcResident>, ENpcResidentAdapter>()
               .AddSingleton<IContentTypeAdapter<Item>, ItemAdapter>()
               .AddSingleton<IContentTypeAdapter<Mount>, MountAdapter>()
               .AddSingleton<IContentTypeAdapter<Status>, StatusAdapter>()
               .AddSingleton<IContentTypeAdapter<Title>, TitleAdapter>()
               .AddSingleton<IContentTypeAdapter<Trait>, TraitAdapter>();
    }
}
