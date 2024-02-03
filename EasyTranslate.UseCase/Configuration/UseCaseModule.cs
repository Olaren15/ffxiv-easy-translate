namespace EasyTranslate.UseCase.Configuration;

using EasyTranslate.UseCase.ItemSearch;
using Microsoft.Extensions.DependencyInjection;

public static class UseCaseModule
{
    public static IServiceCollection AddUseCaseServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<SearchItemByNameCommand>();
        return serviceCollection;
    }
}
