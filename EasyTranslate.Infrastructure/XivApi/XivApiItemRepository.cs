﻿namespace EasyTranslate.Infrastructure.XivApi;

using System.Net.Http.Json;
using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.XivApi.Search;

public class XivApiItemRepository : IItemRepository
{
    public const string HttpClientName = "XivApi";
    private const string SearchEndpoint = "/search";
    private readonly HttpClient httpClient;

    public XivApiItemRepository(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient(HttpClientName);
    }

    public async Task<IEnumerable<Item>> SearchByName(string name, Language searchLanguage)
    {
        var response = await httpClient.PostAsJsonAsync(
                           SearchEndpoint,
                           new
                           {
                               indexes = "",
                               columns = "ID,Icon,Url,Name_en,Name_fr,Name_de,Name_ja",
                               body = new
                               {
                                   query = new
                                   {
                                       wildcard = new Dictionary<string, string>
                                       {
                                           {
                                               searchLanguage switch
                                               {
                                                   Language.English => "NameCombined_en",
                                                   Language.French => "NameCombined_fr",
                                                   Language.German => "NameCombined_de",
                                                   Language.Japanese => "NameCombined_ja",
                                                   var _ => "NameCombined_en",
                                               },
                                               $"*{name}*"
                                           },
                                       },
                                   },
                               },
                           }
                       );
        response.EnsureSuccessStatusCode();
        var parsedResponse = await response.Content.ReadFromJsonAsync<SearchResponse>();

        return parsedResponse!.Results.Select(
            result => new Item(
                result.Id,
                result.IconUrl,
                new Dictionary<Language, string>
                {
                    { Language.English, result.EnglishName },
                    { Language.French, result.FrenchName },
                    { Language.German, result.GermanName },
                    { Language.Japanese, result.JapaneseName },
                },
                result.DetailsUrl
            )
        );
    }
}
