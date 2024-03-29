﻿namespace EasyTranslate.Infrastructure.XivApi;

using System.Net.Http.Json;
using EasyTranslate.Domain.Entities;
using EasyTranslate.Domain.Repositories;
using EasyTranslate.Infrastructure.XivApi.Search;

public class XivApiItemRepository(IHttpClientFactory httpClientFactory) : IItemRepository
{
    public const string HttpClientName = "XivApi";
    private const string SearchEndpoint = "/search";
    private readonly HttpClient httpClient = httpClientFactory.CreateClient(HttpClientName);

    public async Task<IEnumerable<Item>> SearchByName(
        string name,
        Language searchLanguage,
        CancellationToken cancellationToken
    )
    {
        var response = await httpClient.PostAsJsonAsync(
                           SearchEndpoint,
                           new
                           {
                               indexes = "",
                               columns = "ID,IconHD,IconID,IconObjectiveID,Url,Name_en,Name_fr,Name_de,Name_ja",
                               body = new
                               {
                                   query = new
                                   {
                                       @bool = new
                                       {
                                           must = new[]
                                           {
                                               new
                                               {
                                                   wildcard = new Dictionary<string, string>
                                                   {
                                                       {
                                                           GetSearchField(searchLanguage),
                                                           $"*{name.ToLowerInvariant()}*"
                                                       },
                                                   },
                                               },
                                           },
                                           should = new[]
                                           {
                                               new
                                               {
                                                   term = new Dictionary<string, object>
                                                   {
                                                       {
                                                           GetSearchField(searchLanguage),
                                                           new
                                                           {
                                                               value = name.ToLowerInvariant(),
                                                               boost = 10,
                                                           }
                                                       },
                                                   },
                                               },
                                           },
                                       },
                                   },
                                   size = 50,
                               },
                           },
                           cancellationToken
                       );
        response.EnsureSuccessStatusCode();
        var parsedResponse =
            await response.Content.ReadFromJsonAsync<SearchResponse>(cancellationToken: cancellationToken);

        return parsedResponse!.Results.Select(
            result => new Item(
                result.Id,
                result.IconUrl,
                result.IconId ?? result.ContentType?.IconId ?? result.IconObjectiveId,
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

    private static string GetSearchField(Language searchLanguage)
    {
        return searchLanguage switch
        {
            Language.English => "NameCombined_en",
            Language.French => "NameCombined_fr",
            Language.German => "NameCombined_de",
            Language.Japanese => "NameCombined_ja",
            var _ => "NameCombined_en",
        };
    }
}
