namespace EasyTranslate.UseCase.Configuration;

using Domain.Comparers;
using Microsoft.Extensions.DependencyInjection;

public static class UseCaseModule
{
    public static IServiceCollection AddUseCaseServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<SearchContentByNameUseCase>()
                                .AddSingleton<IStringSimilarityComparer, LongestCommonSubstringComparer>();
    }
}
