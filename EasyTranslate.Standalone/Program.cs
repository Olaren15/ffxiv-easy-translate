using System.Text.Json;
using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.XivApi.Configuration;
using EasyTranslate.UseCase.Configuration;
using EasyTranslate.UseCase.ItemSearch;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection().AddInfrastructureServices().AddUseCaseServices().BuildServiceProvider();
var searchByName = serviceCollection.GetService<SearchItemByNameCommand>()!;

var serializerOptions = new JsonSerializerOptions { WriteIndented = true };

while (true)
{
    Console.WriteLine("Search anything:");
    var search = Console.ReadLine() ?? "";
    var results = await searchByName.SearchItemByName(search, Language.English);

    Console.WriteLine(JsonSerializer.Serialize(results, serializerOptions));
}
