using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.GameData;
using EasyTranslate.Infrastructure.GameData.Adapters;
using Lumina.Excel.Sheets;
using Microsoft.Extensions.DependencyInjection;
using Action = Lumina.Excel.Sheets.Action;

namespace EasyTranslate.Infrastructure.Configuration;

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
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<ClassJob>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Companion>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<ContentFinderCondition>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<CraftAction>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Emote>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<ENpcResident>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<EventItem>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Fate>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Item>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Leve>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Mount>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Orchestrion>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<PlaceName>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Quest>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Status>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Title>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Trait>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<Weather>>();
    }

    private static IServiceCollection AddAdapters(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IContentTypeAdapter<Achievement>, AchievementAdapter>()
            .AddSingleton<IContentTypeAdapter<Action>, ActionAdapter>()
            .AddSingleton<IContentTypeAdapter<BNpcName>, BNpcNameAdapter>()
            .AddSingleton<IContentTypeAdapter<ClassJob>, ClassJobAdapter>()
            .AddSingleton<IContentTypeAdapter<Companion>, CompanionAdapter>()
            .AddSingleton<IContentTypeAdapter<ContentFinderCondition>, ContentFinderConditionAdapter>()
            .AddSingleton<IContentTypeAdapter<CraftAction>, CraftActionAdapter>()
            .AddSingleton<IContentTypeAdapter<Emote>, EmoteAdapter>()
            .AddSingleton<IContentTypeAdapter<ENpcResident>, ENpcResidentAdapter>()
            .AddSingleton<IContentTypeAdapter<EventItem>, EventItemAdapter>()
            .AddSingleton<IContentTypeAdapter<Fate>, FateAdapter>()
            .AddSingleton<IContentTypeAdapter<Item>, ItemAdapter>()
            .AddSingleton<IContentTypeAdapter<Leve>, LeveAdapter>()
            .AddSingleton<IContentTypeAdapter<Mount>, MountAdapter>()
            .AddSingleton<IContentTypeAdapter<Orchestrion>, OrchestrionAdapter>()
            .AddSingleton<IContentTypeAdapter<PlaceName>, PlaceNameAdapter>()
            .AddSingleton<IContentTypeAdapter<Quest>, QuestAdapter>()
            .AddSingleton<IContentTypeAdapter<Status>, StatusAdapter>()
            .AddSingleton<IContentTypeAdapter<Title>, TitleAdapter>()
            .AddSingleton<IContentTypeAdapter<Trait>, TraitAdapter>()
            .AddSingleton<IContentTypeAdapter<Weather>, WeatherAdapter>();
    }
}
