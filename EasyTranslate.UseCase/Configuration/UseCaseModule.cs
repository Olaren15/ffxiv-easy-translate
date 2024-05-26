using EasyTranslate.Domain.Comparers;
using Microsoft.Extensions.DependencyInjection;

namespace EasyTranslate.UseCase.Configuration;

public static class UseCaseModule
{
    public static IServiceCollection AddUseCaseServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<SearchContentByNameUseCase>()
            .AddSingleton<IStringSimilarityComparer, LongestCommonSubstringComparer>();
    }
}
