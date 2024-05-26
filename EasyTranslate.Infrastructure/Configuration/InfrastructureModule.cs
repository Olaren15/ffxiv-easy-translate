using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.GameData;
using EasyTranslate.Infrastructure.GameData.Adapters;
using EasyTranslate.Infrastructure.GameData.Sheets;
using Microsoft.Extensions.DependencyInjection;

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
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<AchievementLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<ActionLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<BNpcNameLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<CompanionLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<ContentFinderConditionLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<CraftActionLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<EmoteLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<ENpcResidentLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<FateLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<ItemLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<LeveLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<MountLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<OrchestrionLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<PlaceNameLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<QuestLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<StatusLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<TitleLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<TraitLite>>()
            .AddSingleton<ISearchByNameQuery, SearchByNameQuery<WeatherLite>>();
    }

    private static IServiceCollection AddAdapters(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IContentTypeAdapter<AchievementLite>, AchievementAdapter>()
            .AddSingleton<IContentTypeAdapter<ActionLite>, ActionAdapter>()
            .AddSingleton<IContentTypeAdapter<BNpcNameLite>, BNpcNameAdapter>()
            .AddSingleton<IContentTypeAdapter<CompanionLite>, CompanionAdapter>()
            .AddSingleton<IContentTypeAdapter<ContentFinderConditionLite>, ContentFinderConditionAdapter>()
            .AddSingleton<IContentTypeAdapter<CraftActionLite>, CraftActionAdapter>()
            .AddSingleton<IContentTypeAdapter<EmoteLite>, EmoteAdapter>()
            .AddSingleton<IContentTypeAdapter<ENpcResidentLite>, ENpcResidentAdapter>()
            .AddSingleton<IContentTypeAdapter<FateLite>, FateAdapter>()
            .AddSingleton<IContentTypeAdapter<ItemLite>, ItemAdapter>()
            .AddSingleton<IContentTypeAdapter<LeveLite>, LeveAdapter>()
            .AddSingleton<IContentTypeAdapter<MountLite>, MountAdapter>()
            .AddSingleton<IContentTypeAdapter<OrchestrionLite>, OrchestrionAdapter>()
            .AddSingleton<IContentTypeAdapter<PlaceNameLite>, PlaceNameAdapter>()
            .AddSingleton<IContentTypeAdapter<QuestLite>, QuestAdapter>()
            .AddSingleton<IContentTypeAdapter<StatusLite>, StatusAdapter>()
            .AddSingleton<IContentTypeAdapter<TitleLite>, TitleAdapter>()
            .AddSingleton<IContentTypeAdapter<TraitLite>, TraitAdapter>()
            .AddSingleton<IContentTypeAdapter<WeatherLite>, WeatherAdapter>();
    }
}
