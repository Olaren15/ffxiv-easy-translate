﻿namespace EasyTranslate.Infrastructure.Configuration;

using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.GameData;
using EasyTranslate.Infrastructure.GameData.Adapters;
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
        return serviceCollection.AddSingleton<SearchByNameQuery<Achievement>>().AddSingleton<SearchByNameQuery<Item>>();
    }

    private static IServiceCollection AddAdapters(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<AchievementAdapter>().AddSingleton<ItemAdapter>();
    }
}
