using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasyTranslate.Domain.Entities;
using EasyTranslate.Infrastructure.Configuration;
using EasyTranslate.UseCase;
using EasyTranslate.UseCase.Configuration;
using Lumina;
using Microsoft.Extensions.DependencyInjection;

Console.OutputEncoding = Encoding.UTF8;

var gameDataPath = Environment.GetEnvironmentVariable("GAME_DATA_PATH");
if (gameDataPath is null)
{
    Console.WriteLine(
        @"Please set the GAME_DATA_PATH environment variable to the path of you FFXIV installation: Ex: C:\Program Files (x86)\SquareEnix\FINAL FANTASY XIV- A Realm Reborn\game\sqpack"
    );
    return;
}

var lumina = new GameData(gameDataPath);

var serviceCollection = new ServiceCollection()
                        .AddSingleton(lumina.Excel)
                        .AddInfrastructureServices()
                        .AddUseCaseServices()
                        .BuildServiceProvider();
var searchByName = serviceCollection.GetService<SearchContentByNameUseCase>()!;

var serializerOptions = new JsonSerializerOptions
{
    WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    Converters = { new JsonStringEnumConverter() },
};

while (true)
{
    Console.Write("Search anything: ");
    var search = Console.ReadLine() ?? "";
    var results = await searchByName.Execute(search, Language.English);

    Console.WriteLine(JsonSerializer.Serialize(results, serializerOptions));
}
