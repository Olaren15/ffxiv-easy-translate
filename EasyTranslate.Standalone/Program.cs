using System.Text.Encodings.Web;
using System.Text.Json;
using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.Configuration;
using EasyTranslate.UseCase.Configuration;
using EasyTranslate.UseCase.ItemSearch;
using Lumina;
using Microsoft.Extensions.DependencyInjection;

var lumina = new GameData("/home/cathgilbert/.xlcore/ffxiv/game/sqpack/");

var serviceCollection = new ServiceCollection()
                        .AddSingleton(lumina.Excel)
                        .AddInfrastructureServices()
                        .AddUseCaseServices()
                        .BuildServiceProvider();
var searchByName = serviceCollection.GetService<SearchItemByNameCommand>()!;

var serializerOptions = new JsonSerializerOptions
    { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

while (true)
{
    Console.WriteLine("Search anything:");
    var search = Console.ReadLine() ?? "";
    var results = await searchByName.SearchItemByName(search, Language.English);

    Console.WriteLine(JsonSerializer.Serialize(results, serializerOptions));
}
