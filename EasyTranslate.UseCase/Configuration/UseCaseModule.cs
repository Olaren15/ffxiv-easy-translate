namespace EasyTranslate.UseCase.Configuration;

using ItemSearch;
using Microsoft.Extensions.DependencyInjection;

public static class UseCaseModule
{
    public static IServiceCollection AddUseCaseServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<SearchContentByNameCommand>();
        return serviceCollection;
    }
}
